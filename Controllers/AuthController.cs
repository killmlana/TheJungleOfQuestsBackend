using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheJungleOfQuestsBackend.Entities;
using TheJungleOfQuestsBackend.Helpers;
using TheJungleOfQuestsBackend.Models;

namespace TheJungleOfQuestsBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
        private readonly UserDbContext _context;
        private readonly AuthenticationHelper _authenticationHelper;

        public AuthController(UserDbContext context, AuthenticationHelper authenticationHelper)
        {
            _context = context;
            _authenticationHelper = authenticationHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            User user = _authenticationHelper.CreateUser(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var token = _authenticationHelper.GenerateJwtToken(user);
            return Ok(new { message = "Registration successful", token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }

            // Find the user in the database by email
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userDto.Email.ToLower());

            if (user == null)
            {
                // Return an error if the user does not exist
                return Unauthorized("Email or password is incorrect.");
            }

            // Verify the password 
            if (user.PasswordHash != userDto.Password) // Replace with your hashing verification logic
            {
                return Unauthorized("Email or password is incorrect.");
            }
            
            var token = _authenticationHelper.GenerateJwtToken(user);

            // If login is successful
            return Ok(new { message = "Login successful", token });
        }
}