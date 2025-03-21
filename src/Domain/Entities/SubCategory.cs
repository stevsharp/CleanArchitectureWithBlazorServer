

using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public class SubCategory : BaseAuditableEntity
{
    public string? Name { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = [];
}