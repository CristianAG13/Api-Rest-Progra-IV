using Microsoft.AspNetCore.Mvc;
using WebApiProyect.Services;
using WebApiProyect.Models;

namespace WebApiProyect.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authService = new();

    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuario usuario)
    {
        bool isValid = _authService.ValidateUser(usuario.Username, usuario.Password);

        if (!isValid)
            return Unauthorized("Credenciales inválidas");

      
        return Ok("Login exitoso");
    }
}
