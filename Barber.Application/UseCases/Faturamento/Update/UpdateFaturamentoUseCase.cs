using AutoMapper;
using Barber.Communication.Request;
using Barber.Domain;
using Barber.Domain.Repositories;
using Barber.Exception.ExceptionsBase;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Barber.Application.UseCases.Faturamento.Update;
public class UpdateFaturamentoUseCase : IUpdateFaturamentoUseCase
{
    private readonly IMapper _mapper;
    private readonly IFaturamentoUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFaturamentoUseCase(IMapper mapper, IFaturamentoUpdateOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(int id, RequestFaturamentoJson request)
    {
        Validate(request);
        var faturamento = await _repository.GetById(id);
        if (faturamento is null)
        {
            throw new NotFoundException(message: "Faturamento Não Encontrado");
        }
        _mapper.Map(request, faturamento);
        _repository.Update(faturamento);
        await _unitOfWork.Commit();
    }

    private void Validate(RequestFaturamentoJson request)
    {
        var validator = new FaturamentoValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationFaturamento(errorMessages);
        }
    }
}
