using WebApiProyect.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebApiProyect.Context;

namespace WebApiProyect.Services;

public interface IAuthenticationService
{
    Task<Usuario> AuthenticateAsync(string email, string password);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly AppDbContext _context;

    public AuthenticationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> AuthenticateAsync(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return null;
        }

        // Find the user by email
        var user = await _context.Usuario
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

        if (user == null)
        {
            return null;
        }

        // Check if password matches
        // Note: In a production environment, you should use proper password hashing
        if (user.Password == password)
        {
            // Create a new user object without sending the password back
            var authenticatedUser = new Usuario
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
                // Password is intentionally not included
            };

            return authenticatedUser;
        }

        return null;
    }
}