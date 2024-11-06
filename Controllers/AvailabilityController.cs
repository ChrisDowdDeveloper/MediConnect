using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediConnectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityRepository _availabilityRepository;

        public AvailabilityController(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        [HttpGet("Doctor/{doctorId}")]
        [Authorize]
        public async Task<IActionResult> GetAllAvailabilityByDoctor(string doctorId)
        {
            var availabilities = await _availabilityRepository.GetAllAvailabilityByDoctorAsync(doctorId);
            return Ok(availabilities);
        }

        [HttpGet("Doctor/{doctorId}/Recurring")]
        [Authorize]
        public async Task<IActionResult> GetRecurringAvailabilityByDoctor(string doctorId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != doctorId)
            {
                return Forbid();
            }

            var recurringAvailabilities = await _availabilityRepository.GetRecurringAvailabilityByDoctorAsync(doctorId);
            return Ok(recurringAvailabilities);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAvailabilityById(int id)
        {
            var availability = await _availabilityRepository.GetAvailabilityByIdAsync(id);
            if(availability == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != availability.DoctorId)
            {
                return Forbid();
            }

            return Ok(availability);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAvailability([FromBody] CreateAvailabilityRequestDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != dto.DoctorId)
            {
                return Forbid();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAvailability = new Availability 
            {
                DoctorId = dto.DoctorId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsRecurring = dto.IsRecurring
            };

            var createdAvailability = await _availabilityRepository.CreateAvailabilityAsync(newAvailability);
            return CreatedAtAction(nameof(GetAvailabilityById), new { id = createdAvailability.Id }, createdAvailability);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAvailability(int id, [FromBody] UpdateAvailabilityDto dto)
        {
            
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            var availability = await _availabilityRepository.GetAvailabilityByIdAsync(id);
            if (availability == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != availability.DoctorId)
            {
                return Forbid();
            }
            
            var isDeleted = await _availabilityRepository.DeleteAvailabilityAsync(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }

}