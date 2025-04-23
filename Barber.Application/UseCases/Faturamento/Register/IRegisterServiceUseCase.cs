using Barber.Communication.Request;
using Barber.Communication.Response;

namespace Barber.Application.UseCases.Faturamento.Register;

public interface IRegisterServiceUseCase 
{
    Task<ResponseRegisterFaturamentoJson> Execute(RequestFaturamentoJson request);
}
