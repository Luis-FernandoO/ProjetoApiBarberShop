using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infraestructure.DataAcess;
internal class BarberDbContext : DbContext
{
    public BarberDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Faturamentos> Faturamento { get; set; }

}
