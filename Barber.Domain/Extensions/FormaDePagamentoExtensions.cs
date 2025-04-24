using Barber.Communication.Enum;

namespace Barber.Domain.Extensions;

public static class FormaDePagamentoExtensions
{
    public static string FormaDePagamentoToString(this FormaPagamento formaPagamento)
    {
        return formaPagamento switch
        {
            FormaPagamento.Dinheiro => "Dinheiro",
            FormaPagamento.CartaoCredito => "Cartão de Crédito",
            FormaPagamento.CartaoDebito => "Cartão de Débito",
            FormaPagamento.Pix => "Pix",
            _ => string.Empty
        };
    }
}
