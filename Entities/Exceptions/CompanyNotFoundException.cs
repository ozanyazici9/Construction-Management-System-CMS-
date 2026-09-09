namespace Entities.Exceptions;

public sealed class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(int id)
        : base($"Company with id: {id} could not found") { }
}
