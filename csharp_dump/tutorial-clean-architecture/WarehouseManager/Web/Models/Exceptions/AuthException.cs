namespace Web.Models.Exceptions;

public class AuthException : Exception
{

    public AuthException(string message = "Not authentication. Please log in.")
    {
    }
}
