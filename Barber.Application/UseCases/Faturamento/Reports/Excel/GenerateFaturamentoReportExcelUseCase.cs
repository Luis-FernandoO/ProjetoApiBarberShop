
using Barber.Domain;
using Barber.Domain.Extensions;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using System.Runtime.CompilerServices;

namespace Barber.Application.UseCases.Faturamento.Reports.Excel;

public class GenerateFaturamentoReportExcelUseCase : IGenerateFaturamentoReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IFaturamentoReadOnlyRepository _repository;
    public GenerateFaturamentoReportExcelUseCase(IFaturamentoReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var faturamento = await _repository.FilterByMonth(month);
        if(faturamento.Count == 0)
        {
            return [];
        }
        using var workbook = new XLWorkbook();
        workbook.Author = "BARBEAIA DO LUÍS";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";
        
        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        InsiderHeader(worksheet);

        var line = 2;
        foreach (var item in faturamento)
        {
            worksheet.Cell($"A{line}").Value = item.Servicos.ServicosToString();
            worksheet.Cell($"B{line}").Value = item.Data;
            worksheet.Cell($"C{line}").Value = item.FormaPagamento.FormaDePagamentoToString();
            worksheet.Cell($"D{line}").Value = item.Valor;
            worksheet.Cell($"D{line}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #0, ##0.00";
            worksheet.Cell($"E{line}").Value = item.Descricao;

            line++;
        }
        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();

    }


    private void InsiderHeader(IXLWorksheet worksheets)
    {
        worksheets.Cell("A1").Value = "Serviço";
        worksheets.Cell("B1").Value = "Data";
        worksheets.Cell("C1").Value = "Tipo de Pagamento";
        worksheets.Cell("D1").Value = "Valor";
        worksheets.Cell("E1").Value = "Descrição";

        worksheets.Cells("A1:E1").Style.Font.Bold = true;

        worksheets.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");

        worksheets.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheets.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheets.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheets.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheets.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
