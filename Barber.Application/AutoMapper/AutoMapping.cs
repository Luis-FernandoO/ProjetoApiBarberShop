using AutoMapper;
using Barber.Communication.Request;
using Barber.Communication.Response;
using Barber.Domain.Entities;

namespace Barber.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestFaturamentoJson, Faturamentos>();
    }

    private void EntityToResponse()
    {
        CreateMap<Faturamentos, ResponseRegisterFaturamentoJson>();
        CreateMap<Faturamentos, ResponseShortFaturamentoJson>();
        CreateMap<Faturamentos, ResponseFaturamentoJson>();
    }
}
