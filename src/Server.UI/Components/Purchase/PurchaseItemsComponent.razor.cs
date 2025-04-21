

using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using CleanArchitecture.Blazor.Application.Features.Products.Caching;
using CleanArchitecture.Blazor.Application.Features.Products.Commands.AddEdit;
using CleanArchitecture.Blazor.Application.Features.Products.Queries.GetAll;
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.DTOs;
using CleanArchitecture.Blazor.Infrastructure.Constants;
using CleanArchitecture.Blazor.Server.UI.Pages.Products.Components;

namespace CleanArchitecture.Blazor.Server.UI.Components.Purchase;

public partial class PurchaseItemsComponent
{

    private List<PurchaseItem> _purchaseItemsList = new();

    private PurchaseItem? _selectedItem = new PurchaseItem();

    private IDisposable? _itemCodeSubscription;

    public IEnumerable<PurchaseItem> PurchaseItems => _purchaseItemsList;

    public void SetItems(IEnumerable<PurchaseItemDto> purchaseItemDtos)
    {
        foreach (var purchaseItem in purchaseItemDtos)
        {
            _purchaseItemsList.Add(new PurchaseItem
            {
                Id = purchaseItem.Id,
                ItemId = purchaseItem.ProductId,
                ItemCode = purchaseItem.ItemCode ?? string.Empty,
                Description = purchaseItem.ItemDescription ?? string.Empty,
                Quantity = purchaseItem.Quantity,
                Unit = purchaseItem.Unit,
                Color = purchaseItem.Color,
                UnitPrice = purchaseItem.UnitPrice
            });
        }


        StateHasChanged();
    }

    private decimal NetTotal => _purchaseItemsList.Sum(item =>
        item.Quantity * item.UnitPrice);

    private decimal VatTotal => _purchaseItemsList.Sum(item =>
        item.Quantity * item.UnitPrice * item.VatPercentage / 100);

    private decimal GrandTotal => NetTotal + VatTotal;

    private void SubscribeToItemCodeChanges()
    {
        _itemCodeSubscription?.Dispose(); // Unsubscribe previous, if any
        _itemCodeSubscription = _selectedItem?.ItemCodeChanged.Subscribe(async newValue =>
        {
            if (string.IsNullOrWhiteSpace(newValue))
                return;

            ProductCacheKey.Refresh();

            await OnItemCodeChanged(newValue);
        });
    }

    protected async Task OnItemCodeChanged(string newValue)
    {
        if (string.IsNullOrWhiteSpace(newValue))
            return;

        var query = new GetProductByCodeQuery { Code = newValue };
        var result = await Mediator.Send(query);

        if (result is not null && _selectedItem is not null)
        {
            _selectedItem.Description = result.Name ?? string.Empty;
            _selectedItem.ItemId = result.Id;
            StateHasChanged();
        }
        else
        {
            var command = new AddEditProductCommand
            {
                Pictures = [], Code = newValue
            };

            await ShowEditFormDialog(string.Format(ConstantString.CreateAnItem, L["Product"]), command);
        }
    }

    private async Task ShowEditFormDialog(string title, AddEditProductCommand command)
    {
        var parameters = new DialogParameters<ProductFormDialog>
        {
            { x => x.Model, command }
        };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
        var dialog = await DialogService.ShowAsync<ProductFormDialog>(title, parameters, options);
        var state = await dialog.Result;

        if (state is not null && !state.Canceled)
        {
            var result = state.Data as AddEditProductCommand;

            if (result is not null && _selectedItem is not null)
            {
                _selectedItem.Description = result.Name ?? string.Empty;
                _selectedItem.ItemId = result.Id;
            }
        }
    }

    private async Task Delete(int id)
    {
        var item = _purchaseItemsList.FirstOrDefault(x => x.ItemId == id);
        if (item is not null)
        {
            _purchaseItemsList.Remove(item);
            await Task.CompletedTask; // Simulate async operation
        }
    }

    protected void AddNewRow()
    {
        _selectedItem = new PurchaseItem();
        SubscribeToItemCodeChanges();   
        _purchaseItemsList.Add(_selectedItem);
    }

    public void Dispose()
    {
        _itemCodeSubscription?.Dispose();
    }
}

public sealed class PurchaseItem
{
    private readonly Subject<string> _itemCodeSubject = new();
    public IObservable<string> ItemCodeChanged => _itemCodeSubject.AsObservable();

    private string _itemCode = string.Empty;

    [Description("Item code")]
    public string ItemCode
    {
        get => _itemCode;
        set
        {
            _itemCode = value;
            _itemCodeSubject.OnNext(value);
        }
    }

    public int Id { get; set; } = 0;

    public int ItemId { get; set; } = 0;
    public string Description { get; set; } = string.Empty; 
    public int Quantity { get; set; } = 1;
    public string? Unit { get; set; } = "S";
    public string? Color { get; set; } = "Black";
    public decimal UnitPrice { get; set; } = 0.0m;
    public decimal VATAmount => UnitPrice * Quantity * VatPercentage / 100;
    public int VatPercentage { get; set; } = 24;
}
