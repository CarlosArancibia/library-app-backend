using Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
  private readonly IConfiguration _config;

  public AuthController(IConfiguration config)
  {
    _config = config;
  }

  [HttpPost("login")]
  public IActionResult Login([FromBody] LoginRequest request)
  {

    var user = FakeUserStore.Users
        .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

    if (user.Username == null)
    {
      return Unauthorized("Credenciales inválidas");
    }

    var token = GenerateToken(user.Username, user.Role);
    return Ok(new { token });
  }

  [Authorize]
  [HttpGet("renew")]
  public IActionResult RenewToken()
  {
      var username = User.Identity?.Name;
      var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "User";

      var token = GenerateToken(username!, role);
      return Ok(new { token, username });
  }

  private string GenerateToken(string username, string role)
  {
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
