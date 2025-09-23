using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class CostItem : BaseAuditableEntity
{
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    public int? VendorId { get; set; }
    public Vendor? Vendor { get; set; }
    public string Description { get; set; } = default!;
    public decimal Quantity { get; set; } = 1;
    public decimal UnitCost { get; set; }
    public decimal TotalCost { get; set; }
    public string? Category { get; set; }
}



