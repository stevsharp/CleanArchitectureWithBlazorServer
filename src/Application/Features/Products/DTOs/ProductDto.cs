
using CleanArchitecture.Blazor.Application.Features.PicklistSets.DTOs;
using CleanArchitecture.Blazor.Application.Features.SubProducts.DTOs;
using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Application.Features.Products.DTOs;

[Description("Products")]
public record ProductDto
{

    [Description("Id")] public int Id { get; set; }

    [Description("Product Name")] public string? Name { get; set; }

    [Description("Product Code")] public string? Code { get; set; }

    [Description("Description")] public string? Description { get; set; }

    [Description("Unit")] public string? Unit { get; set; }

    [Description("Color")] public string? Color { get; set; }

    [Description("Color")] public string? Properties { get; set; }

    [Description("Brand Name")] public string? Brand { get; set; }

    [Description("Price")] public decimal Price { get; set; }

    [Description("Pictures")] 
    public List<ProductImage>? Pictures { get; set; }

    [Description("RetailPrice")] 
    public decimal? RetailPrice { get; set; }

    [Description("Stock")]  
    public int? Stock { get; set; }

    [Description("AdditionalInfo")]
    public string? AdditionalInfo { get; set; }

    [Description("ProductUrl")]
    public string? ProductUrl { get; set; }

    [Description("ImageUrl")]
    public string? ImageUrl { get; set; }

    [Description("Category Id")]
    public int CategoryId { get; set; }

    [Description("SubProducts")]
    public  IEnumerable<SubProduct>? SubProducts { get; set; } = [];

    [Description("ColorOptions")]
    public IEnumerable<ProductColorOptionDto> UnitOptions { get; set; } =[];

    [Description("ColorOptions")]
    public IEnumerable<ProductUnitOptionDto> ColorOptions { get; set; } = [];
}

public record ProductUnitOptionDto
{
    public int ProductId { get; set; }
    public int UnitId { get; set; } // Foreign Key
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? Description { get; set; }

}

public record ProductColorOptionDto
{
    public int ProductId { get; set; }
    public int UnitId { get; set; } // Foreign Key
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? Description { get; set; }

}