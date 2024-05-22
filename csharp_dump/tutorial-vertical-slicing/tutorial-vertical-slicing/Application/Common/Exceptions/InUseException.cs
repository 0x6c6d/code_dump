namespace Application.Common.Exceptions;
public class InUseException : Exception
{
    public InUseException(string name, object key)
        : base($"{name} ({key}) is in use and can't be deleted")
    {
    }
}
