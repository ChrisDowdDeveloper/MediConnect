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
        public AppointmentController(ApplicationDBContext context, IAppointmentRepository appointmentRepository)
        {
            _context = context;
            _appointmentRepository = appointmentRepository;
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
                    AppointmentDate = createAppointmentDto.AppointmentDate,
                    AppointmentTime = createAppointmentDto.AppointmentTime,
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

            if (appointment.AppointmentDate != appointmentDto.AppointmentDate || appointment.AppointmentTime != appointmentDto.AppointmentTime)
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

        [HttpDelete("{id}")]
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
            appointment.LastUpdatedDate = DateTime.UtcNow;

            var result = await _appointmentRepository.UpdateAppointmentAsync(appointment);
            if(result != null && result.AppointmentId > 0)
            {
                return Ok(new { message = "Appointment successfully canceled", appointment = result});
            }
            else
            {
                return BadRequest(new { message = "Failed to cancel appointment" });
            }
        }
    }
}