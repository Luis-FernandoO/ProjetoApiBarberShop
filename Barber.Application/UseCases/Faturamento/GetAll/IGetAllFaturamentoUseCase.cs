using Barber.Communication.Response;

namespace Barber.Application.UseCases.Faturamento.GetAll;
public interface IGetAllFaturamentoUseCase
{
    Task<ResponseFaturamentosJson> Execute();
}
