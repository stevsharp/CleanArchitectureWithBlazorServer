using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class Project : BaseAuditableEntity
{
    public int? QuoteId { get; set; }
    public Quote? Quote { get; set; }

    [MaxLength(200)] public string Name { get; set; } = default!;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public decimal? Budget { get; set; }

    public int OwnerId { get; set; }
    public Employee Owner { get; set; } = default!;

    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    public ICollection<CostItem> Costs { get; set; } = new List<CostItem>();
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}



