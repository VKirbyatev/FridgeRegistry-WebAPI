namespace FridgeRegistry.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) 
        : base($"Entity \"{name}\" not found by key: {key}") { }
}