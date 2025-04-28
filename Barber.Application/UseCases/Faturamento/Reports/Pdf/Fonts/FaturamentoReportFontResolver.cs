using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
using System.Reflection;

namespace Barber.Application.UseCases.Faturamento.Reports.Pdf.Fonts;
public class FaturamentoReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);
        stream ??= ReadFontFile(FontHelper.DEFALT_FONT);   

        var length = (int)stream!.Length;
        var data = new byte[length];

        stream.Read(buffer: data, offset: 0, count: length);

        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold , bool italic )
    {
        new Font
        {
            Name = FontHelper.ROBOTO_REGULAR
        };

        return new FontResolverInfo(familyName);
    }


    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"Barber.Application.UseCases.Faturamento.Reports.Pdf.Fonts.{faceName}.ttf");
    }
}
