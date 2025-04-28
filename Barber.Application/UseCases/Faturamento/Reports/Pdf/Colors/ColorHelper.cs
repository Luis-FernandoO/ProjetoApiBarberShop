using DocumentFormat.OpenXml.Drawing;
using Microsoft.EntityFrameworkCore.Metadata;
using MigraDoc.DocumentObjectModel;

namespace Barber.Application.UseCases.Faturamento.Reports.Pdf.Colors;

public class ColorHelper
{
    public static readonly Color GREEN_DARK = Color.Parse("#1A2B2B");
    public static readonly Color GRAY_DARK = Color.Parse("#D7DFDF");
    public static readonly Color GRAY_LIGHT = Color.Parse("#EDF3F3");
    public static readonly Color GRAY_TEXT = Color.Parse("#7D7D7D");

    public static readonly Color GREEN_LIGHT = Color.Parse("#205858");
    public static readonly Color WHITE = Color.Parse("#FFFFFF");
    public static readonly Color BLACK = Color.Parse("#000000");
};
