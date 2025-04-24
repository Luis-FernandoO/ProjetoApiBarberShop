using AutoMapper;
using Barber.Communication.Response;
using Barber.Domain;
using Barber.Exception.ExceptionsBase;
using System.Drawing;

namespace Barber.Application.UseCases.Faturamento.GetById;
public class GetByIdFaturamentoUseCase : IGetByIdFaturamentoUseCase
{
    private readonly IFaturamentoReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdFaturamentoUseCase(IFaturamentoReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseFaturamentoJson> Execute(int id)
    {
        var result = await _repository.GetById(id);
        if(result is null)
            throw new NotFoundException(message:"Faturamento Não Encontrado!");
        return _mapper.Map<ResponseFaturamentoJson>(result);
    }
}
