using System;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.TimeSlot;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        // GET: api/TimeSlot/available?doctorId={doctorId}&date={date}
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTimeSlots([FromQuery] string doctorId, [FromQuery] DateTime date)
        {
            var timeSlots = await _timeSlotRepository.GetAvailableTimeSlotsAsync(doctorId, date);
            return Ok(timeSlots);
        }

        // GET: api/TimeSlot/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeSlotById(int id)
        {
            var timeSlot = await _timeSlotRepository.GetTimeSlotByIdAsync(id);
            if (timeSlot == null)
                return NotFound();

            var responseDto = TimeSlotMapper.ToResponseDto(timeSlot);
            return Ok(responseDto);
        }

        // POST: api/TimeSlot
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> CreateTimeSlot([FromBody] CreateTimeSlotRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var timeSlot = await _timeSlotRepository.CreateTimeSlotAsync(dto);
            var responseDto = TimeSlotMapper.ToResponseDto(timeSlot);

            return CreatedAtAction(nameof(GetTimeSlotById), new { id = responseDto.Id }, responseDto);
        }

        // PUT: api/TimeSlot/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateTimeSlot(int id, [FromBody] UpdateTimeSlotDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _timeSlotRepository.UpdateTimeSlotAsync(id, dto);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            var responseDto = TimeSlotMapper.ToResponseDto(result.UpdatedTimeSlot);
            return Ok(responseDto);
        }

        // DELETE: api/TimeSlot/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            var result = await _timeSlotRepository.DeleteTimeSlotAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return NoContent();
        }

        // POST: api/TimeSlot/book
        [HttpPost("book")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> BookTimeSlot([FromBody] CreateTimeSlotRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _timeSlotRepository.BookTimeSlotAsync(request.Id, patientId);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            var responseDto = TimeSlotMapper.ToResponseDto(result.BookedTimeSlot);
            return Ok(responseDto);
        }
    }
}
