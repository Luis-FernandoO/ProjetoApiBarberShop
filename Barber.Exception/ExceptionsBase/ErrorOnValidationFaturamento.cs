using System.Net;

namespace Barber.Exception.ExceptionsBase;

public class ErrorOnValidationFaturamento : BarberException
{
    private readonly List<string> _errors;
    public override int StatusCode => (int) HttpStatusCode.BadRequest;

    public ErrorOnValidationFaturamento(List<string> errorMessages) : base(string.Empty)
    {
        _errors = errorMessages;
    }

    public override List<string> GetErrors()
    {
        return _errors;
    }
}
