using Barber.Domain.Entities;

namespace Barber.Domain;

public interface IFaturamentoWriteOnlyRepository
{
    Task Add(Faturamentos faturamento);
    Task<bool> Delete(int id);
}
