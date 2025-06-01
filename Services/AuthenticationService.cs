using  WebApiProyect.Models;

namespace WebApiProyect.Services;

public interface IAuthenticationService
{
    Task<Usuario> AuthenticateAsync(string email, string password);
}

public class AuthenticationService : IAuthenticationService
{

    public async Task<Usuario> AuthenticateAsync(string email, string password)
    {
        if (email == "admin" && password == "1234")
        {
            return new Usuario() { Id = 1, Email = email, Password = password, Role = "admin" };
        }

        return null;
    }

}
