
using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public class SubProduct : BaseAuditableEntity
{
    public int? ProductId { get; set; } // Foreign Key
    public string? Unit { get; set; }
    public string? Color { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; } = 0;
    public decimal? RetailPrice { get; set; }
    public Product? Product { get; set; } = null!;
}