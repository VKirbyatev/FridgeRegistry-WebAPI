namespace FridgeRegistry.Application.Common.Exceptions;

public class ForbiddenResourceException : Exception
{
    public ForbiddenResourceException() : base($"You're not allowed to see this resource") {}
}