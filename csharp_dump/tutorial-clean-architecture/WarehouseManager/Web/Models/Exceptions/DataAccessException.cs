namespace Web.Models.Exceptions;

public class DataAccessException : Exception
{
    public int StatusCode { get; private set; }
    public string Response { get; private set; }

    public DataAccessException(string message, int statusCode, string response)
    {
        StatusCode = statusCode;
        Response = response ?? "";
    }
}
