using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class EquipmentItem : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public string? Category { get; set; }
    public decimal? DailyRate { get; set; }
    public decimal? PurchaseCost { get; set; }
    public int? VendorId { get; set; }
    public Vendor? Vendor { get; set; }
}



