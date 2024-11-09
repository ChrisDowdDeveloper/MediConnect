using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Mappers;
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
            var availabilitiesDto = availabilities.Select(AvailabilityMapper.ToDto);
            return Ok(availabilitiesDto);
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
            var availabilitiesDto = recurringAvailabilities.Select(AvailabilityMapper.ToDto);
            return Ok(availabilitiesDto);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAvailabilityById(int id)
        {
            var availability = await _availabilityRepository.GetAvailabilityByIdAsync(id);
            if (availability == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != availability.DoctorId)
            {
                return Forbid();
            }

            var availabilityDto = AvailabilityMapper.ToDto(availability);
            return Ok(availabilityDto);
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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAvailability = AvailabilityMapper.ToModel(dto);

            var createdAvailability = await _availabilityRepository.CreateAvailabilityAsync(newAvailability);
            var availabilityDto = AvailabilityMapper.ToDto(createdAvailability);

            return CreatedAtAction(nameof(GetAvailabilityById), new { id = createdAvailability.Id }, availabilityDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAvailability(int id, [FromBody] UpdateAvailabilityDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID in URL does not match ID in DTO");
            }

            var availability = await _availabilityRepository.GetAvailabilityByIdAsync(id);
            if (availability == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != availability.DoctorId)
            {
                return Forbid();
            }

            // Update the availability using the mapper
            AvailabilityMapper.UpdateModel(availability, dto);

            // Update in repository
            await _availabilityRepository.UpdateAvailabilityAsync(availability);

            // Optionally return the updated availability
            var availabilityDto = AvailabilityMapper.ToDto(availability);
            return Ok(availabilityDto);
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
