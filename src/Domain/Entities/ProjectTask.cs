using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class ProjectTask : BaseAuditableEntity
{
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;

    public int? AssignedToId { get; set; }
    public Employee? AssignedTo { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
}



