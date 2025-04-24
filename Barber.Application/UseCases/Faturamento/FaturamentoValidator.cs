using Barber.Communication.Request;
using FluentValidation;
using System.Drawing;

namespace Barber.Application.UseCases.Faturamento;

public class FaturamentoValidator : AbstractValidator<RequestFaturamentoJson>
{
    public FaturamentoValidator()
    {
        RuleFor(faturamento => faturamento.Servicos).IsInEnum().WithMessage("Adicione o tipo do serviço realizado");
        RuleFor(faturamento => faturamento.Data).LessThanOrEqualTo(DateTime.Now).WithMessage("Não pode ser uma data para o futuro");
        RuleFor(faturamento => faturamento.FormaPagamento).IsInEnum().WithMessage("Adicione alguma forma de pagamento") ;
        RuleFor(faturamento => faturamento.Valor).GreaterThan(0).WithMessage("Valor tem que ser maior que zero");
    }
}
