using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || userId != id)
            {
                return Forbid();
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

            var user = await _patientRepository.GetPatientByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = string.IsNullOrEmpty(patientDto.FirstName) ? user.FirstName : patientDto.FirstName;
            user.LastName = string.IsNullOrEmpty(patientDto.LastName) ? user.LastName : patientDto.LastName;
            user.DateOfBirth = patientDto.DateOfBirth ?? user.DateOfBirth;
            user.Gender = string.IsNullOrEmpty(patientDto.Gender) ? user.Gender : patientDto.Gender;
            user.Address = string.IsNullOrEmpty(patientDto.Address) ? user.Address : patientDto.Address;
            user.PhoneNumber = string.IsNullOrEmpty(patientDto.PhoneNumber) ? user.PhoneNumber : patientDto.PhoneNumber;
            user.EmergencyContactFirstName = string.IsNullOrEmpty(patientDto.EmergencyContactFirstName)
                ? user.EmergencyContactFirstName
                : patientDto.EmergencyContactFirstName;
            user.EmergencyContactLastName = string.IsNullOrEmpty(patientDto.EmergencyContactLastName)
                ? user.EmergencyContactLastName
                : patientDto.EmergencyContactLastName;
            user.EmergencyContactPhoneNumber = string.IsNullOrEmpty(patientDto.EmergencyContactPhoneNumber)
                ? user.EmergencyContactPhoneNumber
                : patientDto.EmergencyContactPhoneNumber;

            try
            {
                var updatedPatient = await _patientRepository.UpdatePatientAsync(user);
                if (updatedPatient == null)
                {
                    return BadRequest("Patient not updated");
                }

                return Ok(PatientMapper.ToDto(updatedPatient));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
