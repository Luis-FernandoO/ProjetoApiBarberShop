using Barber.Application.UseCases.Faturamento.Reports.Excel;
using Barber.Application.UseCases.Faturamento.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Barber.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("Excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromServices] IGenerateFaturamentoReportExcelUseCase useCase, [FromHeader] DateOnly month)
    {
        byte[] files = await useCase.Execute(month);
        
        if(files.Length > 0)
            return File(files, MediaTypeNames.Application.Octet,"report.xlsx");
        return NoContent();

    }

    [HttpGet("Pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf([FromServices] IGenerateFaturamentoReportPdfUseCase useCase, [FromHeader] DateOnly month)
    {
        byte[] files = await useCase.Execute(month);

        if (files.Length > 0)
            return File(files, MediaTypeNames.Application.Pdf, "report.pdf");
        return NoContent();
    }

}
