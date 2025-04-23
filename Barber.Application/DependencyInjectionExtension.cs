using Barber.Application.AutoMapper;
using Barber.Application.UseCases.Faturamento.Delete;
using Barber.Application.UseCases.Faturamento.GetAll;
using Barber.Application.UseCases.Faturamento.GetById;
using Barber.Application.UseCases.Faturamento.Register;
using Barber.Application.UseCases.Faturamento.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Barber.Application;

public static class DependencyInjectionExtension
{

    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);

    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    public static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetAllFaturamentoUseCase, GetAllFaturamentUseCase>();
        services.AddScoped<IGetByIdFaturamentoUseCase, GetByIdFaturamentoUseCase>();
        services.AddScoped<IRegisterServiceUseCase, RegisterServiceUseCase>();
        services.AddScoped<IUpdateFaturamentoUseCase, UpdateFaturamentoUseCase>();
        services.AddScoped<IDeleteFaturamentoUseCase, DeleteFaturamentoUseCase>();

    }


}
