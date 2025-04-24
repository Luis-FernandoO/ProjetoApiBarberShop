using System.Net;

namespace Barber.Exception.ExceptionsBase;
public class NotFoundException : BarberException
{
    public NotFoundException(string message) : base(message) {}
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
