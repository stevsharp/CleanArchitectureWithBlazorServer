
using CleanArchitecture.Blazor.Application.Features.PicklistSets.DTOs;
using CleanArchitecture.Blazor.Application.Features.SubProducts.DTOs;


namespace CleanArchitecture.Blazor.Server.UI.Components.Products;

public partial class ProductMatrix
{
    protected IEnumerable<PicklistSetDto> OptionsColors = [];
    protected IEnumerable<PicklistSetDto> OptionsUnits = [];
    protected List<SubProductDto> Variants = [];

    public IEnumerable<SubProductDto> GetVariants() => Variants;

    [EditorRequired]
    [Parameter]
    public IEnumerable<SubProductDto> SubProducts { get; set; } = [];

    [EditorRequired]
    [Parameter]
    public int ProductId { get;set; } = 0;
    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        OptionsColors = [.. PicklistService.DataSource.Where(x => x.Name == Picklist.Color)];
        OptionsUnits = [.. PicklistService.DataSource.Where(x => x.Name == Picklist.Unit)];
    }

    protected override Task OnParametersSetAsync()
    {
        Variants = [.. (from color in OptionsColors.Select(x => x.Value).Distinct()
                    from unit in OptionsUnits.Select(x => x.Value).Distinct()
                    let existing = SubProducts.FirstOrDefault(v => v.Color == color && v.Unit == unit)
                    select existing ?? new SubProductDto
                    {
                        Id = existing?.Id ?? 0,
                        ProductId = ProductId,
                        Color = color,
                        Unit = unit,
                        Stock = existing?.Stock ?? 0,
                        Price = existing?.Price ?? 0,
                        RetailPrice = existing?.RetailPrice ?? 0
                    })];

        StateHasChanged();

        return Task.CompletedTask;
    }

}
