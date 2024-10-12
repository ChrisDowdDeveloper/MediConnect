using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Doctor;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediConnectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {

        private readonly UserManager<Doctor> _userManager;

        public DoctorController(UserManager<Doctor> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterDoctor([FromBody] CreateDoctorRequestDto createDoctorDto)
        {

            if(ModelState.IsValid)
            {
                var doctor = new Doctor
                {
                    UserName = createDoctorDto.UserName,
                    Email = createDoctorDto.Email,
                    FirstName = createDoctorDto.FirstName,
                    LastName = createDoctorDto.LastName,
                    Specialty = createDoctorDto.Specialty,
                    Availability = createDoctorDto.Availability,
                    YearsOfExperience = createDoctorDto.YearsOfExperience,
                    OfficeAddress = createDoctorDto.OfficeAddress
                };

                var result = await _userManager.CreateAsync(doctor, createDoctorDto.Password);

                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(doctor, "Doctor");
                    return Ok(new { message = "Doctor registered successfully" });
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