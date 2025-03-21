
using CleanArchitecture.Blazor.Application.Features.Categories.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.SubCategories.DTOs;

[Description("SubCategories")]
public class SubCategoryDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name {get;set;}

    [Description("IsChecked")]
    public bool IsChecked { get; set; }

    [Description("Categories")]
    public List<CategoryDto>? Categories {get;set;} 

}

