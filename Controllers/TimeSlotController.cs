using System;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.TimeSlot;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MediConnectBackend.Models;

namespace MediConnectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public TimeSlotController(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        [HttpGet("Doctor/{doctorId}")]
        public async Task<IActionResult> GetAllTimeSlotsByDoctor(string doctorId)
        {
            var timeSlots = await _timeSlotRepository.GetAllTimeSlotsByDoctorAsync(doctorId);
            return Ok(timeSlots);
        }

        [HttpGet("Doctor/{doctorId}/Available")]
        public async Task<IActionResult> GetAllAvailableTimeSlotsByDoctor(string doctorId)
        {
            var availableTimeSlots = await _timeSlotRepository.GetAllAvailableTimeSlotsByDoctorAsync(doctorId);
            return Ok(availableTimeSlots);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeSlotById(int id)
        {
            var timeSlot = await _timeSlotRepository.GetTimeSlotByIdAsync(id);
            if (timeSlot == null)
                return NotFound();

            return Ok(timeSlot);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<IActionResult> CreateTimeSlot([FromBody] CreateTimeSlotRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newTimeSlot = new TimeSlot
            {
                DoctorId = dto.DoctorId,
                StartDateTime = dto.StartDateTime,
                EndDateTime = dto.EndDateTime,
                IsBooked = dto.IsBooked
            };

            var createdTimeSlot = await _timeSlotRepository.CreateTimeSlotAsync(newTimeSlot);
            return CreatedAtAction(nameof(GetTimeSlotById), new { id = createdTimeSlot.Id }, createdTimeSlot);
        }
        

        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateTimeSlot(int id, [FromBody] UpdateTimeSlotDto dto)
        {
            if (id != dto.Id || !ModelState.IsValid)
                return BadRequest();

            var timeSlot = await _timeSlotRepository.GetTimeSlotByIdAsync(id);
            if (timeSlot == null)
                return NotFound();

            var updatedTimeSlot = await _timeSlotRepository.UpdateTimeSlotAsync(timeSlot);
            return Ok(updatedTimeSlot);
        }
        

        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            var isDeleted = await _timeSlotRepository.DeleteTimeSlotAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }    
            return NoContent();
        }
    }
}
