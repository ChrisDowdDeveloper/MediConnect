using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Login;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MediConnectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(IJwtTokenManager jwtTokenManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _jwtTokenManager = jwtTokenManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User? user = null;

            if (loginDto.UserNameOrEmail.Contains('@'))
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            }

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            
            if (!passwordCheck)
            {
                return Unauthorized("Password does not match.");
            }

            var token = _jwtTokenManager.Authenticate(user);

            return Ok(new { token });
        }
    }
}
