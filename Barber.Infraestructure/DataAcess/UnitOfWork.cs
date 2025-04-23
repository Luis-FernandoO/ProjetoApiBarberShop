using Barber.Domain.Repositories;

namespace Barber.Infraestructure.DataAcess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly BarberDbContext _context;
    public UnitOfWork(BarberDbContext context)
    {
        _context = context;
    }
    public async Task Commit() => await _context.SaveChangesAsync();
}
