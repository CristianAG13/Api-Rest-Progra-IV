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
            return Unauthorized(new { message = "Credenciales inválidas" });

        var token = TokenGenerator.GenerateToken(validuser, jwtSettings);

        Response.Cookies.Append("auth_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // ✅ para producción HTTPS
            SameSite = SameSiteMode.None,
            Path = "/",
            MaxAge = TimeSpan.FromDays(1)
        });

        return Ok(new { message = "Login exitoso" });
    }


    [HttpGet("/auth/check")]
    public IActionResult CheckAuth()
    {
        return Ok(new { isAuthenticated = true });
    }

    [AllowAnonymous]
    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("auth_token", new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // ❗️ Poner en false si estás usando http://localhost
            SameSite = SameSiteMode.None,
            Path = "/"     // ❗️ Debe coincidir con el path usado al crearla
        });

        return Ok(new { message = "Sesión cerrada correctamente" });
    }
}