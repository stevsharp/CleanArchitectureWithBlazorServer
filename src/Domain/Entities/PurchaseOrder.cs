using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class PurchaseOrder : BaseAuditableEntity
{
    public int VendorId { get; set; }
    public Vendor Vendor { get; set; } = default!;

    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;

    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Draft;

    public string? ExternalNumber { get; set; }
    public string? Notes { get; set; }
}



