
using CleanArchitecture.Blazor.Application.Features.Products.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.OfferLines.DTOs;

[Description("OfferLines")]
public record OfferLineDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Offer id")]
    public int OfferId {get;set;} 
    [Description("Item id")]
    public int ItemId {get;set;}

    [Description("Item code")]
    public string? ItemCode { get; set; }
    [Description("Item description")]
    public string? ItemDescription { get; set; }

    [Description("Quantity")]
    public int Quantity {get;set;} 
    [Description("Discount")]
    public decimal Discount {get;set;} 
    [Description("Line total")]
    public decimal LineTotal {get;set;}

    [Description("Line Price")]
    public decimal? LinePrice { get; set; }

    [Description("Unit")]
    public string? Unit { get; set; }
    [Description("Color")]
    public string? Color { get; set; }

    [Description("SubItemId")]
    public int SubItemId { get; set; }

    [Description("Product")]
    public ProductDto? Product { get; set; }

    [Description("VAT Percentage")]
    public decimal? VATPercentage { get; set; } = 0m;

    [Description("VAT Amount")]
    public decimal? VATAmount { get; set; } = 0m;

}

