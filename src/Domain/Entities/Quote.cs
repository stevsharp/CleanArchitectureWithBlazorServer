using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class Quote : BaseAuditableEntity
{
    public int CompanyId { get; set; }
    public Company Company { get; set; } = default!;
    public string Title { get; set; } = default!;
    public int OwnerId { get; set; }           // Employee
    public Employee Owner { get; set; } = default!;

    public QuoteStatus Status { get; set; }
    public DateTime ValidUntil { get; set; }
    public string Currency { get; set; } = "EUR";
    public decimal TotalAmount { get; set; }
    public decimal? MarginPct { get; set; }

    public ICollection<QuoteVersion> Versions { get; set; } = [];
    public ICollection<QuoteAttachment> Attachments { get; set; } = [];
    public ICollection<QuoteApproval> Approvals { get; set; } = [];
}



