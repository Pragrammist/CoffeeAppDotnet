namespace Domain.Exceptions;

public class CoffeeApplicationException : Exception
{
    public int StatusCode { get; }
    public CoffeeApplicationException(string message, int code) : base(message) => StatusCode = code;
}
public class UserDataNotValidException : CoffeeApplicationException
{
    public UserDataNotValidException(object data) : base($"User is not valid: {data}", 400) { }
}

public class DataNotValidException : CoffeeApplicationException
{
    public DataNotValidException(object data) : base($"Data invalid: {data}", 400) { }
}

public class PermissionDenied : CoffeeApplicationException
{
    public PermissionDenied(object data) : base($"Perrmission denied: {data}", 400) { }
}

public class NotFoundException : CoffeeApplicationException
{
    public NotFoundException(object data) : base($"id was not found: {data}", 404) { }
}

