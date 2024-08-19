using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingAPI.Entities;
using OnlineShoppingAPI.Repository;
using OnlineShoppingAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        // Updated constructor to inject IConfiguration
        public AuthenticateController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration; // Initialize _configuration
        }

        [HttpGet, Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                return StatusCode(200, users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("Registraion")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.UserId = "U" + new Random().Next(1000, 9999);

                    await _userRepository.Register(user);
                    return StatusCode(200, user);
                }
                else
                {
                    return BadRequest("Enter valid Details!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("ValidUser")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> ValidUser(Login login)
        {
            AuthResponse authReponse = null;
            var user = await _userRepository.ValidUser(login.Email, login.Password);
            if (user != null)
            {
                authReponse = new AuthResponse()
                {
                    UserId = user.UserId,
                    Role = user.Role,
                    UserName = user.Name,
                    Mobile = user.Mobile,
                    Token = GetToken(user),
                };
            }

            return Ok(authReponse);
        }

        [HttpPut, Route("EditProduct")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Edit([FromBody] User user)
        {
            try
            {
                await _userRepository.UpdateUser(user);
                return StatusCode(200, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("Deleteuser")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GetToken(User user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            // Header section
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );

            // Payload section
            var subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
            });

            var expires = DateTime.UtcNow.AddMinutes(10); // Token will expire after 10 minutes

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            // Generate token using tokenDescriptor
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
