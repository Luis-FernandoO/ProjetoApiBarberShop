using Barber.Application.UseCases.Faturamento.Reports.Pdf.Colors;
using Barber.Application.UseCases.Faturamento.Reports.Pdf.Fonts;
using Barber.Domain;
using Barber.Domain.Extensions;
using DocumentFormat.OpenXml.VariantTypes;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;
using System.Xml;

namespace Barber.Application.UseCases.Faturamento.Reports.Pdf;

public class GenerateFaturamentoReportPdfUseCase : IGenerateFaturamentoReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_SERVICE_TABLE = 25;
    private readonly IFaturamentoReadOnlyRepository _repository;

    public GenerateFaturamentoReportPdfUseCase(IFaturamentoReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new FaturamentoReportFontResolver();
    }
    public async Task<byte[]> Execute(DateOnly month) 
    {
        var faturamento = await _repository.FilterByMonth(month);
        if (faturamento.Count == 0)
        {
            return [];
        }
        var document = CreateDocument(month);
        var page = CreatePage(document);
        var totalExpenses = faturamento.Sum(x => x.Valor);

        CreateHeaderWithProfilePhotoAndName(page);
        CreateTotalSpentSection(page, month, totalExpenses);
        foreach (var item in faturamento) 
        {
            var table = CreateExpenseTable(page);
            var row = table.AddRow();
            row.Height = HEIGHT_ROW_SERVICE_TABLE;
            AddExpenseTitle(row.Cells[0], item.Servicos.ServicosToString());
            AddHeaderForAmount(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_SERVICE_TABLE;
            row.Cells[0].AddParagraph(item.Data.ToString("D"));
            SetStyleBaseForExpenseInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 10;

            row.Cells[1].AddParagraph(item.Data.ToString("t"));
            SetStyleBaseForExpenseInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(item.FormaPagamento.FormaDePagamentoToString());
            SetStyleBaseForExpenseInformation(row.Cells[2]);

            AddAmountForExpense(row.Cells[3], item.Valor);

            if (!string.IsNullOrWhiteSpace(item.Descricao))
            {
                var descricaoRow = table.AddRow();
                descricaoRow.Height = HEIGHT_ROW_SERVICE_TABLE;

                descricaoRow.Cells[0].AddParagraph(item.Descricao);
                descricaoRow.Cells[0].Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorHelper.GRAY_TEXT};
                descricaoRow.Cells[0].Shading.Color = ColorHelper.GRAY_LIGHT;
                descricaoRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descricaoRow.Cells[0].MergeRight = 2;
                descricaoRow.Cells[0].Format.LeftIndent = 10;

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        }
        return RenderDocument(document);

    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"Faturamento Mês - {month}";
        document.Info.Author = "Luís";
        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.ROBOTO_MEDIUM;
        return document;
    }
    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        return section;
    }
    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();
        var assembly = Assembly.GetExecutingAssembly();

        var directoryName = Path.GetDirectoryName(assembly.Location);

        var pathFile = Path.Combine(directoryName!, "Images", "img_barber.png");

        row.Cells[0].AddImage(pathFile);
        row.Cells[1].AddParagraph("BARBEARIA DO LUIS");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 25};
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }
    private void CreateTotalSpentSection(Section page, DateOnly month, decimal totalSpent)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format("Total de Faturamento", month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.ROBOTO_MEDIUM, Size = 15 });
        paragraph.AddLineBreak();
        paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalSpent}", new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 50 });
    }
    private Table CreateExpenseTable(Section page)
    {
        var table= page.AddTable();
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }
    private void AddExpenseTitle(Cell cell, string title)
    {
        cell.AddParagraph(title);
        cell.Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 15, Color = ColorHelper.WHITE };
        cell.Shading.Color = ColorHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 10;
    }
    private void AddHeaderForAmount(Cell cell) 
    {
        cell.AddParagraph("VALOR");
        cell.Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 15, Color = ColorHelper.WHITE };
        cell.Shading.Color = ColorHelper.GREEN_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void SetStyleBaseForExpenseInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.GRAY_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void AddAmountForExpense(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{CURRENCY_SYMBOL} {amount}");
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }
    private byte[] RenderDocument(Document document)
    {
        var render = new PdfDocumentRenderer
        {
            Document = document
        };
        render.RenderDocument();
        using var file = new MemoryStream();
        render.PdfDocument.Save(file);
        return file.ToArray();
    }
}

