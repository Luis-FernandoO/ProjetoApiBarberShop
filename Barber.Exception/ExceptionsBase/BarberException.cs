namespace Barber.Exception.ExceptionsBase;
public abstract class BarberException : SystemException
{
    protected BarberException(string message) : base(message)
    {
        
    }
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
