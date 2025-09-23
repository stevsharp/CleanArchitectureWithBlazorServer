using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class QuoteVersion : BaseAuditableEntity
{
    public int QuoteId { get; set; }
    public Quote Quote { get; set; } = default!;

    public int VersionNumber { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public int CreatedByEmployeeId { get; set; }
    public Employee CreatedByEmployee { get; set; } = default!;

    public ICollection<QuoteLine> Lines { get; set; } = [];
}



