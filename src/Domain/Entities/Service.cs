using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class Service : BaseAuditableEntity
{
    public int ServiceCategoryId { get; set; }
    public ServiceCategory Category { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public PricingModelType DefaultPricingModel { get; set; }
    public ICollection<ServiceVariant> Variants { get; set; } = [];
}



