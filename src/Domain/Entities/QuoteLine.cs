using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public sealed class QuoteLine : BaseAuditableEntity
{
    public int QuoteVersionId { get; set; }
    public QuoteVersion Version { get; set; } = default!;

    public int ServiceId { get; set; }
    public Service Service { get; set; } = default!;

    public int? VariantId { get; set; }
    public ServiceVariant? Variant { get; set; }

    public PricingModelType PricingModel { get; set; }

    public decimal Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }

    // Venue relation (optional per line, e.g., catering)
    public int? VenueId { get; set; }
    public Venue? Venue { get; set; }

    // Model-specific optional fields
    public int? Pax { get; set; }
    public decimal? MediaBudget { get; set; }
    public decimal? AgencyFeePct { get; set; }
}



