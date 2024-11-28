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
            var timeSlotsDto = timeSlots.Select(TimeSlotMapper.ToDto).ToList();
            return Ok(timeSlotsDto);
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

        [HttpPost("{doctorId}")]
        [Authorize]
        public async Task<IActionResult> CreateTimeSlot(string doctorId, [FromBody] CreateTimeSlotRequestDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (dto.DoctorId != userId)
            {
                return Forbid();
            }
            
            if(string.IsNullOrEmpty(dto.DoctorId))
            {
                return BadRequest("The doctor ID cannot be empty.");
            }

            var newTimeSlot = TimeSlotMapper.ToTimeSlotFromCreateDTO(doctorId, dto);
            var createdTimeSlot = await _timeSlotRepository.CreateTimeSlotAsync(newTimeSlot);
            return CreatedAtAction(nameof(GetTimeSlotById), new { id = createdTimeSlot.Id }, TimeSlotMapper.ToDto(createdTimeSlot));
        }
        

        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateTimeSlot(int id, [FromBody] UpdateTimeSlotDto dto)
        {
            var updatedTimeSlot = await _timeSlotRepository.UpdateTimeSlotAsync(id, dto);
            if(updatedTimeSlot == null)
            {
                return NotFound("Time Slot cannot be found");
            }

            return Ok(TimeSlotMapper.ToDto(updatedTimeSlot));
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
