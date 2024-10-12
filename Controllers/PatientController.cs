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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PatientController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPatient([FromBody] CreatePatientRequestDto createPatientDto)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    UserName = createPatientDto.UserName,
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

                // Creating the patient using IdentityUser's UserManager
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
    }
}
