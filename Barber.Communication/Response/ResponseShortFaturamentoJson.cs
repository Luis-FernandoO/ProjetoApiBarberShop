using Barber.Communication.Enum;

namespace Barber.Communication.Response;

public class ResponseShortFaturamentoJson
{
    public int Id { get; set; }
    public Servicos Servicos{ get; set; }
    public decimal Valor { get; set; }
}
