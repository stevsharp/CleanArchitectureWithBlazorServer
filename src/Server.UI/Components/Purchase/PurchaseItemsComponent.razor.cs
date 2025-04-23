

using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using CleanArchitecture.Blazor.Application.Features.OfferLines.DTOs;
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

    private PurchaseItem? _selectedItem = new();

    private IDisposable? _itemCodeSubscription;
    private IDisposable? _itemUnitSubscription;
    private IDisposable? _itemColorSubscription;

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

    public void SetItems(IEnumerable<OfferLineDto>  offerLineDtos)
    {
        foreach (var offerLine in offerLineDtos)
        {
            _purchaseItemsList.Add(new PurchaseItem
            {
                Id = offerLine.Id,
                ItemId = offerLine.ItemId,
                ItemCode = offerLine.ItemCode ?? string.Empty,
                Description = offerLine.ItemDescription ?? string.Empty,
                Quantity = offerLine.Quantity,
                Unit = offerLine.Unit,
                Color = offerLine.Color,
                UnitPrice = offerLine.LinePrice ?? 0m
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
        if (string.IsNullOrWhiteSpace(newValue) || _selectedItem is null)
            return;

        var query = new GetProductByColorQuery
        {
            Code = newValue,
            Color = _selectedItem.Color ?? string.Empty,
            Unit = _selectedItem?.Unit ?? string.Empty
        };
        var result = await Mediator.Send(query);

        if (result is not null && _selectedItem is not null)
        {
            _selectedItem.Description = result.Name ?? string.Empty;
            _selectedItem.ItemId = result.Id;
            _selectedItem.UnitPrice = result?.SubProducts?.FirstOrDefault()?.Price ?? 0;
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
        SubscribeToUnitChanges();
        SubscribeToColorChanges();
        _purchaseItemsList.Add(_selectedItem);
    }

    private void SubscribeToUnitChanges()
    {
        _itemUnitSubscription?.Dispose(); // Dispose previous subscription if any

        _itemUnitSubscription = _selectedItem?.UnitChanged.Subscribe(newValue =>
        {
            if (string.IsNullOrWhiteSpace(newValue))
                return;

            ProductCacheKey.Refresh();

            InvokeAsync(async () =>
            {
                var query = new GetProductByColorQuery
                {
                    Color = newValue,
                    Unit = _selectedItem?.Color ?? string.Empty,
                    Code = _selectedItem?.ItemCode ?? string.Empty
                };

                var result = await Mediator.Send(query);

                if (result is not null && _selectedItem is not null)
                {
                    _selectedItem.UnitPrice = result?.SubProducts?.FirstOrDefault()?.Price ?? 0;
                }

                // You can handle the result here (e.g., update _selectedItem based on result)
                Console.WriteLine($"Color changed to: {newValue}");

                StateHasChanged();
            });
        });
    }

    private void SubscribeToColorChanges()
    {
        _itemColorSubscription?.Dispose();

        _itemColorSubscription = _selectedItem?.ColorChanged.Subscribe(newValue =>
        {
            if (string.IsNullOrWhiteSpace(newValue))
                return;

            ProductCacheKey.Refresh();

            _ = Task.Run(async () =>
            {
                var query = new GetProductByColorQuery
                {
                    Color = newValue,
                    Unit = _selectedItem?.Unit ?? string.Empty,
                    Code = _selectedItem?.ItemCode ?? string.Empty
                };

                var result = await Mediator.Send(query);

                if (result is not null && _selectedItem is not null)
                {
                    _selectedItem.UnitPrice = result?.SubProducts?.FirstOrDefault()?.Price ?? 0; 
                }

                Console.WriteLine($"Color changed to: {newValue}");

                await InvokeAsync(StateHasChanged);
            });
        });
    }





    public void Dispose()
    {
        _itemCodeSubscription?.Dispose();
        _itemUnitSubscription?.Dispose();
        _itemColorSubscription?.Dispose();
    }
}

public sealed class PurchaseItem
{
    private readonly Subject<string> _itemCodeSubject = new();
    public IObservable<string> ItemCodeChanged => _itemCodeSubject.AsObservable();

    private readonly Subject<string> _unitSubject = new();
    public IObservable<string> UnitChanged => _unitSubject.AsObservable();

    private readonly Subject<string> _colorSubject = new();
    public IObservable<string> ColorChanged => _colorSubject.AsObservable();

    private string _itemCode = string.Empty;
    private string? _unit = "S";
    private string? _color = "Black";

    [Description("Item code")]
    public string ItemCode
    {
        get => _itemCode;
        set
        {
            if (value is not null && _itemCode != value)
            {
                _itemCode = value;
                _itemCodeSubject.OnNext(value);
            }

        }
    }

    public string? Unit
    {
        get => _unit;
        set
        {
            if (value is not null && _unit != value)
            {
                _unit = value;
                _unitSubject.OnNext(value);
            }

        }
    }

    public string? Color
    {
        get => _color;
        set
        {
            if (value != null && _color != value)
            {
                _color = value;
                _colorSubject.OnNext(value);
            }
        }
    }

    public int Id { get; set; } = 0;
    public int ItemId { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;

    public decimal UnitPrice { get; set; } = 0.0m;
    public decimal VATAmount => UnitPrice * Quantity * VatPercentage / 100;
    public int VatPercentage { get; set; } = 24;
}

