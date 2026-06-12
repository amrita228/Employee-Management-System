using BCrypt.Net;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(
            ApplicationDbContext context,
            TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (await _context.Users.AnyAsync(x => x.Username == dto.Username))
            {
                return BadRequest("User already exists");
            }

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("User Registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == dto.Username);

            if (user == null)
                return Unauthorized("Invalid Username");

            bool verified = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

            if (!verified)
                return Unauthorized("Invalid Password");

            var token = _tokenService.CreateToken(user);

            return Ok(new
            {
                token
            });
        }
    }
}