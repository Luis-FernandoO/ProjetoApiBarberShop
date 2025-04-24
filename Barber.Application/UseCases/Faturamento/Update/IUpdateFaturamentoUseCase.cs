using Barber.Communication.Request;

namespace Barber.Application.UseCases.Faturamento.Update;

public interface IUpdateFaturamentoUseCase
{
    Task Execute(int id, RequestFaturamentoJson request);
}
