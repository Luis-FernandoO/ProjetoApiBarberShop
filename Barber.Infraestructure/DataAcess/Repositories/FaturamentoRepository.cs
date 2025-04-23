using Barber.Domain;
using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infraestructure.DataAcess.Repositories;
internal class FaturamentoRepository : IFaturamentoReadOnlyRepository, IFaturamentoWriteOnlyRepository, IFaturamentoUpdateOnlyRepository
{
    private readonly BarberDbContext _context;
    public FaturamentoRepository(BarberDbContext context)
    {
        _context = context;
    }

    public async Task Add(Faturamentos faturamento)
    {
        await _context.Faturamento.AddAsync(faturamento);
    }

    public async Task<bool> Delete(int id)
    {
        var result = await _context.Faturamento.FirstOrDefaultAsync(x => x.Id == id);
        if (result is null)
        {
            return false;
        }
        _context.Faturamento.Remove(result);

        return true;
    }

    public async Task<List<Faturamentos>> FilterByMonth(DateOnly data)
    {
        var startDate = new DateTime(year: data.Year, month: data.Month, day: 1);
        var daysInMonth = DateTime.DaysInMonth(year : data.Year, month : data.Month);
        var endDate = new DateTime(year: data.Year, month: data.Month, day: daysInMonth, hour : 23, minute :59, second: 59);
        return await _context
            .Faturamento
            .AsNoTracking()
            .Where(x => x.Data >= startDate && x.Data <= endDate)
            .OrderBy(x => x.Data)
            .ThenBy(x => x.Servicos)
            .ToListAsync();
    }

    public async Task<List<Faturamentos>> GetAll()
    {
        return await _context.Faturamento.ToListAsync();
    }

    public async Task<Faturamentos?> GetById(int id)
    {
        return await _context.Faturamento.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    }

    public void Update(Faturamentos faturamento)
    {
        _context.Faturamento.Update(faturamento);
    }
}
