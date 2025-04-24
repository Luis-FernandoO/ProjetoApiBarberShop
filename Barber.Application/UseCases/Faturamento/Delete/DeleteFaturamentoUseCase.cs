
using Barber.Domain;
using Barber.Domain.Repositories;
using Barber.Exception.ExceptionsBase;

namespace Barber.Application.UseCases.Faturamento.Delete;
public class DeleteFaturamentoUseCase : IDeleteFaturamentoUseCase
{
    private readonly IFaturamentoWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFaturamentoUseCase(IFaturamentoWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;   
    }


    public async Task Execute(int id)
    {
        var result = await _repository.Delete(id);

        if (!result)
        {
            throw new NotFoundException(message: "Faturamento Não Encontrado!");
        }
        await _unitOfWork.Commit();
    }
}
