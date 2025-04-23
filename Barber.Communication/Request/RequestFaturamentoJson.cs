using Barber.Communication.Enum;

namespace Barber.Communication.Request;

public class RequestFaturamentoJson
{
    public Servicos Servicos { get; set; }  
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public string? Descricao { get; set; } = string.Empty;
}
