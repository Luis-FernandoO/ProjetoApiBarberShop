﻿using AutoMapper;
using Barber.Communication.Request;
using Barber.Communication.Response;
using Barber.Domain;
using Barber.Domain.Entities;
using Barber.Domain.Repositories;
using Barber.Exception.ExceptionsBase;

namespace Barber.Application.UseCases.Faturamento.Register;

public class RegisterServiceUseCase : IRegisterServiceUseCase
{
    private readonly IFaturamentoWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterServiceUseCase(IFaturamentoWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseRegisterFaturamentoJson> Execute(RequestFaturamentoJson request)
    {
        Validate(request);
        var entity = _mapper.Map<Faturamentos>(request);

        await _repository.Add(entity);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterFaturamentoJson>(entity);
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
