using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class QuoteAttachment : BaseAuditableEntity
{
    public int QuoteId { get; set; }
    public Quote Quote { get; set; } = default!;

    [MaxLength(260)] public string FileName { get; set; } = default!;
    [MaxLength(1000)] public string FilePath { get; set; } = default!;
    public DateTime UploadedAtUtc { get; set; } = DateTime.UtcNow;
}



