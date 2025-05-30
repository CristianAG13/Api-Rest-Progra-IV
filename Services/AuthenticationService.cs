namespace WebApiProyect.Services;

public class AuthenticationService
{
    public bool ValidateUser(string username, string password)
    {
        // Usuario hardcoded
        return username == "admin" && password == "1234";
    }
}


