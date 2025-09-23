using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class QuoteApproval : BaseAuditableEntity
{
    public int QuoteId { get; set; }
    public Quote Quote { get; set; } = default!;

    public int ApproverId { get; set; }   // Employee
    public Employee Approver { get; set; } = default!;

    public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;
    public DateTime? ApprovedAtUtc { get; set; }
    public string? Comment { get; set; }
}



