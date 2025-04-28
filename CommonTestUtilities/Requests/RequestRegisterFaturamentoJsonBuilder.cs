using Barber.Communication.Enum;
using Barber.Communication.Request;
using Bogus;

namespace CommonTestUtilities.Requests;
public class RequestRegisterFaturamentoJsonBuilder
{
    public static RequestFaturamentoJson Build()
    {
        return new Faker<RequestFaturamentoJson>()
            .RuleFor(r => r.Servicos, f => f.PickRandom<Servicos>())
            .RuleFor(r => r.Data, f => f.Date.Past())
            .RuleFor(r => r.FormaPagamento, f => f.PickRandom<FormaPagamento>())
            .RuleFor(r => r.Valor, f => f.Random.Decimal(1, 10000));
    }
}
