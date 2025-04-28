using Barber.Communication.Response;
using Barber.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Barber.API.Filters;
public class FaturamentoFilter : IExceptionFilter
{
  
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is BarberException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var barberException = context.Exception as BarberException;
        var errorResponse = new ResponseErrorJson(barberException!.GetErrors());
        context.HttpContext.Response.StatusCode = barberException.StatusCode;
        context.Result = new ObjectResult(errorResponse);

    }
    
    private void ThrowUnknowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(context.Exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
