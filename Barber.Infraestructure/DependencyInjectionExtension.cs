using Barber.Domain;
using Barber.Domain.Repositories;
using Barber.Infraestructure.DataAcess;
using Barber.Infraestructure.DataAcess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Barber.Infraestructure;

public static class DependencyInjectionExtension
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFaturamentoReadOnlyRepository, FaturamentoRepository>();
        services.AddScoped<IFaturamentoWriteOnlyRepository, FaturamentoRepository>();
        services.AddScoped<IFaturamentoUpdateOnlyRepository, FaturamentoRepository>();
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BarberDbContext>(options => options.UseSqlServer(connectionString));
    }
}
