using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class Venue : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public string? Location { get; set; }
    public int? Capacity { get; set; }
    public decimal? VenueFee { get; set; }
}



