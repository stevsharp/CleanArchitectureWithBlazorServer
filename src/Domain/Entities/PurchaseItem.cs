

using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public class PurchaseItem : BaseAuditableEntity
{

    public int InvoiceId { get; set; }
    public string ItemCode { get; set; } = string.Empty;

    public int ProductId { get; set; } = 0;
    public string ItemDescription { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string? Color { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal VATPercentage { get; set; }
    public decimal VATAmount { get; set; }
    public decimal TotalAmount { get; set; }

    public PurchaseInvoice Invoice { get; set; } = null!;
}
