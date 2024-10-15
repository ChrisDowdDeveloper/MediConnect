using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Helpers;
using MediConnectBackend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediConnectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastAppointmentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IPastAppointmentRepository _pastAppointmentRepository;
        public PastAppointmentController(ApplicationDBContext context, IPastAppointmentRepository pastAppointmentRepository)
        {
            _context = context;
            _pastAppointmentRepository = pastAppointmentRepository;
        }

        [HttpGet("patient/{patientId}")]
        [Authorize]
        public async Task<IActionResult> GetPastAppointmentsByPatientId([FromRoute] string patientId, [FromQuery] PastAppointmentQueryObject query)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (query.DoctorId != userId && query.PatientId != userId)
            {
                return Forbid();
            }

            query.PatientId = patientId;

            var pastAppointments = await _pastAppointmentRepository.GetAllPastAppointmentsByPatientIdAsync(query);

            if (pastAppointments == null || pastAppointments.Count == 0)
            {
                return NotFound(new { message = $"No past appointments found for patient with ID: {patientId}" });
            }

            return Ok(pastAppointments);
        }

        [HttpGet("doctor/{doctorId}")]
        [Authorize]
        public async Task<IActionResult> GetPastAppointmentsByDoctorId([FromRoute] string doctorId, [FromQuery] PastAppointmentQueryObject query)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (doctorId != userId)
            {
                return Forbid();
            }
            query.DoctorId = doctorId;

            var pastAppointments = await _pastAppointmentRepository.GetAllPastAppointmentsByDoctorIdAsync(query);

            if (pastAppointments == null || pastAppointments.Count == 0)
            {
                return NotFound(new { message = "No past appointments found for the specified doctor." });
            }

            return Ok(pastAppointments);
        }


    }
}