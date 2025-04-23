using Barber.Domain.Entities;

namespace Barber.Domain;
public interface IFaturamentoUpdateOnlyRepository
{
    Task<Faturamentos?> GetById(int id);
    void Update(Faturamentos faturamento);
}
