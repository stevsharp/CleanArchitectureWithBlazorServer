using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class ServiceVariant : BaseAuditableEntity
{
    public int ServiceId { get; set; }
    public Service Service { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Unit { get; set; }
    public int? ComplexityLevel { get; set; }
    public decimal? BasePrice { get; set; }
}



