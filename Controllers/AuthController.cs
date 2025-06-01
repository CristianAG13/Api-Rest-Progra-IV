using WebApiProyect.Models;
using WebApiProyect.Entities;
using WebApiProyect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiProyect.Helpers;


namespace WebApiProyect.Controllers;

public record TokenResponse(string token);

public record Credendials(string Email, string Password);

[Authorize]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthenticationService authenticationService;

    private readonly JwtSettings jwtSettings;

    public AuthController(IAuthenticationService _authenticationService, JwtSettings _jwtSettings)
    {
        authenticationService = _authenticationService;
        jwtSettings = _jwtSettings;
    }

    [HttpPost("/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(Credendials user)
    {
        var validuser = await authenticationService.AuthenticateAsync(user.Email, user.Password);
        if (validuser is null)
            return Unauthorized();


        var token = TokenGenerator.GenerateToken(validuser, jwtSettings);

        return Ok(new TokenResponse(token));
    }

}
