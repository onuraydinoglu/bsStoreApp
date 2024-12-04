namespace Entities.Exceptions;

public abstract partial class NotFoundException : Exception
{
    protected NotFoundException(string message) : base(message){ }
}
