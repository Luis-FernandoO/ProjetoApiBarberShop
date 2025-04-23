using Barber.Application.UseCases.Faturamento.Register;
using Barber.Communication.Request;
using Barber.Communication.Response;
using Barber.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoApiBarbeariaRocketseat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FaturamentoController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterFaturamentoJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterServiceUseCase useCase, [FromBody]RequestFaturamentoJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

}
