
using CleanArchitecture.Blazor.Application.Features.SubCategories.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Categories.DTOs;

[Description("Categories")]
public class CategoryDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string? Name { get; set; }

    [Description("Comments")]
    public string? Comments { get; set; }

    [Description("Sub Categories")]
    public List<SubCategoryDto>? SubCategories { get; set; } = new();
}

