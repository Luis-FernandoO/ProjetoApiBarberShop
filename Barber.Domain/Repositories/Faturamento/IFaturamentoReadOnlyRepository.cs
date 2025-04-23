using Barber.Domain.Entities;

namespace Barber.Domain;
public interface IFaturamentoReadOnlyRepository
{
    Task<List<Faturamentos>> GetAll();
    Task<Faturamentos?> GetById(int id);
    Task<List<Faturamentos>>FilterByMonth(DateOnly data);   
}
