using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

// ====================== Vendors / Venues / Equipment ======================
public sealed class Vendor : BaseAuditableEntity
{
    public int CompanyId { get; set; }
    public Company Company { get; set; } = default!;
    public int? ServiceCategoryId { get; set; }
    public ServiceCategory? ServiceCategory { get; set; }
    public string? Terms { get; set; }
}



