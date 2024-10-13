using System.Security.Claims;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Doctor;
using MediConnectBackend.Helpers;
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
    public class DoctorController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(UserManager<User> userManager, IDoctorRepository doctorRepository)
        {
            _userManager = userManager;
            _doctorRepository = doctorRepository;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDoctor([FromBody] CreateDoctorRequestDto createDoctorDto)
        {
            if (ModelState.IsValid)
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

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(doctor, "Doctor");
                    return Ok(new { message = "Doctor registered successfully" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors([FromQuery] DoctorQueryObject query)
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync(query);
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(string id)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(DoctorMapper.ToDto(doctor));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateDoctor(string id, [FromBody] UpdateDoctorDto doctorDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || userId != id)
            {
                return Forbid();
            }

            if (await _userManager.FindByIdAsync(id) is not Doctor doctor)
            {
                return NotFound("Doctor cannot be found");
            }

            DoctorMapper.UpdateModel(doctor, doctorDto);

            var result = await _userManager.UpdateAsync(doctor);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Doctor updated successfully" });

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || userId != id)
            {
                return Forbid();
            }

            bool isDeleted = await _doctorRepository.DeleteDoctorAsync(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound("Doctor not found");
            }
        }
    }
}
