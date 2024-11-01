using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
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
        public async Task<IActionResult> GetAllAvailabilityByDoctor(string doctorId)
        {
            var availabilities = await _availabilityRepository.GetAllAvailabilityByDoctorAsync(doctorId);
            return Ok(availabilities);
        }

        [HttpGet("Doctor/{doctorId}/Recurring")]
        public async Task<IActionResult> GetRecurringAvailabilityByDoctor(string doctorId)
        {
            var recurringAvailabilities = await _availabilityRepository.GetRecurringAvailabilityByDoctorAsync(doctorId);
            return Ok(recurringAvailabilities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAvailabilityById(int id)
        {
            var availability = await _availabilityRepository.GetAvailabilityByIdAsync(id);
            if(availability == null)
            {
                return NotFound();
            }
            return Ok(availability);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAvailability([FromBody] CreateAvailabilityRequestDto dto)
        {
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
        public async Task<IActionResult> UpdateAvailability(int id, [FromBody] UpdateAvailabilityDto dto)
        {
            if (id != dto.Id || !ModelState.IsValid)
                return BadRequest();

            var availability = await _availabilityRepository.GetAvailabilityByIdAsync(id);
            if (availability == null)
                return NotFound();

            availability.DayOfWeek = dto.DayOfWeek ?? availability.DayOfWeek;
            availability.StartTime = dto.StartTime ?? availability.StartTime;
            availability.EndTime = dto.EndTime ?? availability.EndTime;
            availability.IsRecurring = dto.IsRecurring ?? availability.IsRecurring;

            var updatedAvailability = await _availabilityRepository.UpdateAvailabilityAsync(availability);
            return Ok(updatedAvailability);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            var isDeleted = await _availabilityRepository.DeleteAvailabilityAsync(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }

}