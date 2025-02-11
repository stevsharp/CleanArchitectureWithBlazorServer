using CleanArchitecture.Blazor.Application.Features.Contacts.DTOs;
using CleanArchitecture.Blazor.Application.Features.InvoiceLines.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Invoices.DTOs;

[Description("Invoices")]
public record InvoiceDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Offer id")]
    public int? OfferId { get; set; }
    [Description("Invoice date")]
    public DateTime InvoiceDate { get; set; }
    [Description("Total amount")]
    public decimal TotalAmount { get; set; }
    [Description("Status")]
    public string? Status { get; set; }

    [Description("ShippingCosts")]
    public decimal ShippingCosts { get; set; }

    [Description("PaymentType")]
    public string? PaymentType { get; set; } = "Cash";

    [Description("Draft")]
    public int? Draft { get; set; }

    [Description("StatDesignus")]
    public string? Design { get; set; }

    [Description("ShippingMethod")]
    public string? ShippingMethod { get; set; }

    [Description("Packaging")]
    public int? Packaging { get; set; }

    [Description("Invoice lines")]
    public List<InvoiceLineDto>? InvoiceLines { get; set; }

    [Description("Contact")]
    public ContactDto Contact { get; set; } = null!;
}

