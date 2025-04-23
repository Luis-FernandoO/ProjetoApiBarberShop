namespace Barber.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}
