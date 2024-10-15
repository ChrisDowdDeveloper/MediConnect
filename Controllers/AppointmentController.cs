using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Dtos.Appointment;
using MediConnectBackend.Helpers;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Mappers;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediConnectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPastAppointmentRepository _pastAppointmentRepository;
        public AppointmentController(ApplicationDBContext context, IAppointmentRepository appointmentRepository, IPastAppointmentRepository pastAppointmentRepository)
        {
            _context = context;
            _appointmentRepository = appointmentRepository;
            _pastAppointmentRepository = pastAppointmentRepository;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByAppointmentId([FromRoute] int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);

            if(appointment == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appointment.DoctorId != userId && appointment.PatientId != userId)
            {
                return Forbid();
            }

            return Ok(AppointmentMapper.ToDto(appointment));
        }

        [HttpGet("patient/{patientId}")]
        [Authorize]
        public async Task<IActionResult> GetAppointmentByPatient([FromRoute] string patientId, [FromQuery] AppointmentQueryObject query)
        {
            query.PatientId = patientId;

            var appointments = await _appointmentRepository.GetAllAppointmentsByPatientIdAsync(query);

            if(appointments == null || appointments.Count == 0)
            {
                return NotFound($"No appointments found for patient with ID: {patientId}");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (query.DoctorId != userId && query.PatientId != userId)
            {
                return Forbid();
            }

            return Ok(appointments);
        }
        
        [HttpGet("doctor/{doctorId}")]
        [Authorize]
        public async Task<IActionResult> GetAppointmentByDoctor([FromRoute] string doctorId, [FromQuery] AppointmentQueryObject query)
        {
            query.DoctorId = doctorId;

            var appointments = await _appointmentRepository.GetAllAppointmentsByDoctorIdAsync(query);

            if(appointments == null || appointments.Count == 0)
            {
                return NotFound($"No appointments found for doctor with ID: {doctorId}");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (query.DoctorId != userId)
            {
                return Forbid();
            }

            return Ok(appointments);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
        {
            if(ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    PatientId = createAppointmentDto.PatientId,
                    DoctorId = createAppointmentDto.DoctorId,
                    AppointmentDateTime = createAppointmentDto.AppointmentDateTime,
                    Notes = createAppointmentDto.Notes
                };

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (appointment.DoctorId != userId && appointment.PatientId != userId)
                {
                    return Forbid();
                }

                var result = await _appointmentRepository.CreateAppointmentAsync(appointment);
                if(result != null && result.AppointmentId > 0)
                {
                    return Ok(new { message = "Appointment created successfully", appointment = result});
                }
                else 
                {
                    return BadRequest(new { message = "Failed to create appointment" });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDto appointmentDto)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);

            if(appointment == null)
            {
                return NotFound(new { message = "Appointment not found" });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appointmentDto.DoctorId != userId && appointmentDto.PatientId != userId)
            {
                return Forbid();
            }

            if (appointment.AppointmentDateTime.Date != appointmentDto.AppointmentDateTime.Date ||
                                    appointment.AppointmentDateTime.TimeOfDay != appointmentDto.AppointmentDateTime.TimeOfDay)
            {
                appointment.AppointmentStatus = AppointmentStatus.RESCHEDULED;
            }

            AppointmentMapper.UpdateModel(appointment, appointmentDto);

            var result = await _appointmentRepository.UpdateAppointmentAsync(appointment);

            if(result != null && result.AppointmentId > 0)
            {
                return Ok(new { message = "Appointment successfully updated" });
            }
            else
            {
                return BadRequest(new { message = "Failed to update appointment" });
            }
        }

        [HttpPut("{doctorId}/{id}")]
        [Authorize]
        public async Task<IActionResult> FinishAppointment(string doctorId, int id, [FromBody] UpdateAppointmentDto appointmentDto)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if(appointment == null)
            {
                return NotFound(new { message = "Appointment not found" });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(doctorId != userId)
            {
                return Forbid();
            }

            appointment.AppointmentStatus = AppointmentStatus.FINISHED;
            AppointmentMapper.UpdateModel(appointment, appointmentDto);

            var updateResult = await _appointmentRepository.UpdateAppointmentAsync(appointment);
            if(updateResult == null || updateResult.AppointmentId <= 0)
            {
                return BadRequest(new { message = "Failed to finish appointment" });
            }

            var pastAppointmentResult = await MoveToPastAppointments(appointment);
            if(pastAppointmentResult != null && pastAppointmentResult.PastAppointmentId > 0)
            {
                var deleteResult = await _appointmentRepository.DeleteAppointmentById(id);
    
                if (deleteResult)
                {
                    return Ok(new { message = "Appointment successfully finished, moved to past appointments, and deleted from active appointments." });
                }
                else
                {
                    return Ok(new { message = "Appointment moved to past appointments, but failed to delete from active appointments." });
                }
            }
            else
            {
                return BadRequest(new { message = "Failed to move appointment to past appointments." });
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if(appointment == null)
            {
                return NotFound(new { message = "Appointment not found" });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appointment.DoctorId != userId && appointment.PatientId != userId)
            {
                return Forbid();
            }

            if (appointment.AppointmentStatus == AppointmentStatus.CANCELED || appointment.AppointmentStatus == AppointmentStatus.FINISHED)
            {
                return BadRequest(new { message = "Canceled or finished appointments cannot be updated" });
            }

            appointment.AppointmentStatus = AppointmentStatus.CANCELED;

            var updateResult = await _appointmentRepository.UpdateAppointmentAsync(appointment);
            if(updateResult == null || updateResult.AppointmentId <= 0)
            {
                return BadRequest(new { message = "Failed to cancel appointment" });
            }

            var pastAppointmentResult = await MoveToPastAppointments(appointment);
            if(pastAppointmentResult != null && pastAppointmentResult.PastAppointmentId > 0)
            {
                var deleteResult = await _appointmentRepository.DeleteAppointmentById(id);
    
                if (deleteResult)
                {
                    return Ok(new { message = "Appointment successfully finished, moved to past appointments, and deleted from active appointments." });
                }
                else
                {
                    return Ok(new { message = "Appointment moved to past appointments, but failed to delete from active appointments." });
                }
            }
            else
            {
                return BadRequest(new { message = "Failed to move appointment to past appointments." });
            }
        }

        private async Task<PastAppointment> MoveToPastAppointments(Appointment appointment)
        {
            var pastAppointment = new PastAppointment
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDateTime = appointment.AppointmentDateTime,
                Notes = appointment.Notes,
                CreationDate = appointment.CreationDate,
                LastUpdatedDate = DateTime.UtcNow,
                CompletionDate = DateTime.UtcNow
            };

            return await _pastAppointmentRepository.CreatePastAppointmentAsync(pastAppointment);
        }

    }
}