using Barber.Application.UseCases.Faturamento.Delete;
using Barber.Application.UseCases.Faturamento.GetAll;
using Barber.Application.UseCases.Faturamento.GetById;
using Barber.Application.UseCases.Faturamento.Register;
using Barber.Application.UseCases.Faturamento.Update;
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
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterServiceUseCase useCase, [FromBody]RequestFaturamentoJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseFaturamentosJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllFaturamentos([FromServices] IGetAllFaturamentoUseCase useCase)
    {
        var response = await useCase.Execute();
        if (response.Faturamentos.Count != 0)
            return Ok(response);
        return NoContent();
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(ResponseFaturamentoJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdFaturamentoUseCase useCase, int id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }


    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteFaturamentoUseCase useCase, [FromRoute] int id)
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof (ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateFaturamentoUseCase useCase,
        [FromRoute] int id, 
        [FromBody] RequestFaturamentoJson request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }



}
