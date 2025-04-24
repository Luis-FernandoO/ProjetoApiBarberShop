using AutoMapper;
using Barber.Communication.Response;
using Barber.Domain;

namespace Barber.Application.UseCases.Faturamento.GetAll;
public class GetAllFaturamentUseCase : IGetAllFaturamentoUseCase
{
    private readonly IFaturamentoReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllFaturamentUseCase(IFaturamentoReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseFaturamentosJson> Execute()
    {
        var result = await _repository.GetAll();
        return new ResponseFaturamentosJson
        {
            Faturamentos = _mapper.Map<List<ResponseShortFaturamentoJson>>(result)
        };
    }
}
