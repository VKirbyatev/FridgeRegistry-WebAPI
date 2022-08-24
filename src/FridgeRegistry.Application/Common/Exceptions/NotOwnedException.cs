namespace FridgeRegistry.Application.Common.Exceptions;

public class NotOwnedException : Exception
{
    public NotOwnedException() : base($"Not owned resource") {}
}