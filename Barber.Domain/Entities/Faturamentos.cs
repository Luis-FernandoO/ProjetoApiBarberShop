using Barber.Communication.Enum;

namespace Barber.Domain.Entities;

public class Faturamentos
{
    public int Id { get; set; }
    public Servicos Servicos { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public string? Descricao { get; set; }
}
