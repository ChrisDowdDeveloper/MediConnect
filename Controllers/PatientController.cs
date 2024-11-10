using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Patient;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Mappers;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediConnectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPatientRepository _patientRepository;

        public PatientController(UserManager<User> userManager, IPatientRepository patientRepository)
        {
            _userManager = userManager;
            _patientRepository = patientRepository;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPatient([FromBody] CreatePatientRequestDto createPatientDto)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    UserName = createPatientDto.Email,
                    Email = createPatientDto.Email,
                    FirstName = createPatientDto.FirstName,
                    LastName = createPatientDto.LastName,
                    DateOfBirth = createPatientDto.DateOfBirth,
                    Gender = createPatientDto.Gender,
                    Address = createPatientDto.Address,
                    EmergencyContactFirstName = createPatientDto.EmergencyContactFirstName,
                    EmergencyContactLastName = createPatientDto.EmergencyContactLastName,
                    EmergencyContactPhoneNumber = createPatientDto.EmergencyContactPhoneNumber
                };

                var result = await _userManager.CreateAsync(patient, createPatientDto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(patient, "Patient");
                    return Ok(new { message = "Patient registered successfully" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPatientById(string id)
        {
            // Log the token claims for debugging
            var userClaims = User.Claims;
            foreach (var claim in userClaims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");  // Inspect claims here
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Ensure this matches 'nameid' in your JWT
            if (string.IsNullOrEmpty(userId) || userId != id)
            {
                return Forbid();  // This will trigger if the token doesn't match the 'id'
            }

            var patient = await _patientRepository.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(PatientMapper.ToDto(patient));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePatient(string id, [FromBody] UpdatePatientDto patientDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || userId != id)
            {
                return Forbid();
            }

            var patient = await _userManager.FindByIdAsync(id) as Patient;

            if (patient == null)
            {
                return NotFound("Patient cannot be found");
            }

            var result = await _userManager.UpdateAsync(patient);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Patient updated successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePatient(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || userId != id)
            {
                return Forbid();
            }

            bool isDeleted = await _patientRepository.DeletePatientAsync(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound("Patient not found");
            }
        }
    }
}
