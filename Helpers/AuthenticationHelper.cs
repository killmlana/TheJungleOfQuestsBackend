using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheJungleOfQuestsBackend.Entities;
using TheJungleOfQuestsBackend.Models;

namespace TheJungleOfQuestsBackend.Helpers;

public class AuthenticationHelper
{
    private readonly IConfiguration _configuration;
    
    public AuthenticationHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public User CreateUser(UserDTO userDto)
    {
        return new User()
        {
            Email = userDto.Email.ToLower(),
            PasswordHash = userDto.Password
        };
    }
    public string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            //new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("userId", user.Id.ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Token expiration time
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}