namespace PPC.RetoRecompensa.Domain.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}
public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}
