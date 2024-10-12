using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MediConnectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
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

            IdentityUser? user = null;

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

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT Secret Key is not configured in appsettings.json");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);

            var expirationInMinutesString = _configuration["JwtSettings:ExpirationInMinutes"];
            if (string.IsNullOrEmpty(expirationInMinutesString))
            {
                throw new InvalidOperationException("JWT ExpirationInMinutes is not configured in appsettings.json");
            }

            int expirationInMinutes = int.Parse(expirationInMinutesString);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}