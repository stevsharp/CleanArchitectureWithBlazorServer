using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

// Optional: if you also keep a PricingModel catalog
public sealed class PricingModel : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public PricingModelType Type { get; set; }
    public string? FormulaExpression { get; set; }
    public decimal? MinFee { get; set; }
    public string? Notes { get; set; }
}



