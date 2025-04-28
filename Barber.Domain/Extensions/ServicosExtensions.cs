using Barber.Communication.Enum;

namespace Barber.Domain.Extensions;

public static class ServicosExtensions
{
    public static string ServicosToString(this Servicos servicos)
    {
        return servicos switch
        {
            Servicos.Cabelo => "Corte",
            Servicos.Barba => "Barba",
            Servicos.BarbaECabelo=> "Barba e Cabelo",
            _ => string.Empty
        };
    }
}
