

using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace CleanArchitecture.Blazor.Infrastructure;

public interface IPrintInvoice : IDocument
{
    byte[] GeneratePdf();
}

public class PrintInvoice : IPrintInvoice
{
    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(20);
            page.Content().Column(column =>
            {
                // Header with Logo and Title
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(innerColumn =>
                    {
                        innerColumn.Item().Text("POLITIS")
                            .FontSize(20)
                            .Bold()
                            .FontColor("#000000"); // Black

                        innerColumn.Item().Text("Εισαγωγές - Αντιπροσωπείες - Εμπόριο\nΜεταξοτυπίες - Κέντημα - Ψηφιακές Εκτυπώσεις")
                            .FontSize(10)
                            .FontColor("#000000"); // Black

                        innerColumn.Item().Text("ΠΕΛΑΣΓΙΑΣ 76 - ΠΕΡΙΣΤΕΡΙ - ΤΗΛ: 210 5758500-3")
                            .FontSize(10)
                            .FontColor("#000000");

                        innerColumn.Item().Text("www.politis-sport.gr")
                            .FontSize(10)
                            .FontColor("#000000")
                            .Underline();
                    });

                    row.ConstantItem(60)
                        .Height(60)
                        .Background("#FFA500") // Approximation of the orange color
                        .AlignCenter()
                        .AlignMiddle()
                        .Text("P")
                        .FontSize(40)
                        .Bold()
                        .FontColor("#FFFFFF"); // White
                });

                // Title
                column.Item().PaddingVertical(10).AlignCenter().Text("ΠΑΡΑΛΗΠΤΗΣ")
                    .FontSize(14)
                    .Bold()
                    .FontColor("#FFA500"); // Orange

                // Form Fields
                column.Item().PaddingTop(10).Column(formColumn =>
                {
                    formColumn.Item().Text("ΟΝΟΜΑ: .......................................................")
                        .FontSize(12)
                        .FontColor("#000000");

                    formColumn.Item().Text("ΔΙΕΥΘΥΝΣΗ: ..................................................")
                        .FontSize(12)
                        .FontColor("#000000");

                    formColumn.Item().Text("ΠΟΛΗ: ...........................................................")
                        .FontSize(12)
                        .FontColor("#000000");

                    formColumn.Item().Text("ΤΗΛ.: ............................................................")
                        .FontSize(12)
                        .FontColor("#000000");
                });
            });
        });
    }

    public byte[] GeneratePdf()
    {
        using var stream = new MemoryStream();
        
        this.GeneratePdf(stream);

        return stream.ToArray();
    }
}
