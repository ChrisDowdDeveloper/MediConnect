using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Patient;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediConnectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        
        private readonly UserManager<Patient> _userManager;

        public PatientController(UserManager<Patient> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPatient([FromBody] CreatePatientRequestDto createPatientDto)
        {

            if(ModelState.IsValid)
            {
                var patient = new Patient
                {
                    Username = createPatientDto.Username,
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

                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(patient, "Patient");
                    return Ok(new { message = "Patient registered successfully" });
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return BadRequest(ModelState);

        }

    }
}