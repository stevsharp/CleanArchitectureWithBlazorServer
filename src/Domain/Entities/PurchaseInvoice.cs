

using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public class PurchaseInvoice : BaseAuditableEntity
{
    public int SupplierId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public string InvoiceType { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal VATAmount { get; set; }
    public string PaymentStatus { get; set; } = "Pending";
    public string? PaymentMethod { get; set; }
    public string? IBAN { get; set; }
    public string? SWIFT { get; set; }
    public string? Notes { get; set; }

    public int? Isfinalized { get; set; } = 0;
    public Supplier Supplier { get; set; } = null!;
    public ICollection<PurchaseItem> Items { get; set; } = [];
}
