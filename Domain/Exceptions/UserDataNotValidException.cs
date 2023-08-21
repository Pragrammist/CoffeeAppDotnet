namespace Domain.Exceptions;

public class CoffeeApplicationException : Exception
{
    public int HttpStatusCode { get; }

    public int MessageStatusCode { get; }

    public CoffeeApplicationException(string message, int code, int messageStatusCode) : base(message)
    {
        HttpStatusCode = code;
        MessageStatusCode = messageStatusCode;
    }
}
public class UserDataNotValidException : CoffeeApplicationException
{
    public UserDataNotValidException(object data, int messageStatusCode) : base($"User is not valid: {data}", 400, messageStatusCode) { }
}

public class DataNotValidException : CoffeeApplicationException
{
    public DataNotValidException(object data, int messageStatusCode) : base($"Data invalid: {data}", 400, messageStatusCode) { }
}

public class PermissionDenied : CoffeeApplicationException
{
    public PermissionDenied(object data, int messageStatusCode) : base($"Perrmission denied: {data}", 400, messageStatusCode) { }
}

public class NotFoundException : CoffeeApplicationException
{
    public NotFoundException(object data) : base($"id was not found: {data}", 404, 405) { }
}

