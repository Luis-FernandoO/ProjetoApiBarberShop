using Barber.Communication.Request;

namespace Barber.Application.UseCases.Faturamento.Update;

public interface IUpdateFaturamentoUseCase
{
    Task<RequestFaturamentoJson> Execute(int id, RequestFaturamentoJson request);
}
