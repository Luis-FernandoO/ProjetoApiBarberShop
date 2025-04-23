using Barber.Communication.Response;

namespace Barber.Application.UseCases.Faturamento.GetById;
public interface IGetByIdFaturamentoUseCase
{
    Task<ResponseFaturamentoJson> Execute(int id);
}
