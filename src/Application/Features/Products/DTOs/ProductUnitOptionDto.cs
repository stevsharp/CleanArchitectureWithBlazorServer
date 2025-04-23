
namespace CleanArchitecture.Blazor.Application.Features.Products.DTOs;

public record ProductUnitOptionDto
{
    public int ProductId { get; set; }
    public int UnitId { get; set; } // Foreign Key
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? Description { get; set; }

}
