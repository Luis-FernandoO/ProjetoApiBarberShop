using Barber.Communication.Request;
using FluentValidation;

namespace Barber.Application.UseCases.Faturamento;

public class FaturamentoValidator : AbstractValidator<RequestFaturamentoJson>
{
    public FaturamentoValidator()
    {
        RuleFor(faturamento => faturamento.Servicos).IsInEnum().WithMessage("Adicione um serviço inválida");
        RuleFor(faturamento => faturamento.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Não pode ser uma data para o futuro");
        RuleFor(faturamento => faturamento.FormaPagamento).IsInEnum().WithMessage("Forma de pagamento inválida");
        RuleFor(faturamento => faturamento.Valor).GreaterThan(0).WithMessage("Valor tem que ser maior que zero");
    }
}
