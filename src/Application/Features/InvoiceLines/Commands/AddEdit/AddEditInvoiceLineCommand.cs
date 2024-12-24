﻿
using System.Reactive.Subjects;
using CleanArchitecture.Blazor.Application.Features.InvoiceLines.Caching;
using CleanArchitecture.Blazor.Application.Features.Products.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.InvoiceLines.Commands.AddEdit;

public class AddEditInvoiceLineCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Invoice id")]
    public int InvoiceId { get; set; }
    int _productId;
    [Description("Product id")]
    public int ProductId
    {
        get => _productId; set
        {

            if (_itemId != value)
            {
                _itemId = value;
                ItemIdBehaviorSubject.OnNext(value);  // Notify on change
            }
        }
    }

    private int _itemId;

    //[Description("Item id")]
    //public int ItemId
    //{
    //    get => _itemId;
    //    set
    //    {
    //        if (_itemId != value)
    //        {
    //            _itemId = value;
    //            ItemIdBehaviorSubject.OnNext(value);  // Notify on change
    //        }
    //    }
    //}

    int _quantity;
    [Description("Quantity")]
    public int Quantity
    {
        get => _quantity; set
        {
            if (_quantity == value) return;

            _quantity = value;
            CalculateLineTotal();
        }
    }

    decimal _unitPrice;
    [Description("Unit price")]
    public decimal UnitPrice
    {
        get => _unitPrice; set
        {
            if (_unitPrice == value) return;

            _unitPrice = value;
            CalculateLineTotal();

        }
    }

    decimal _lineTotal;
    [Description("Line total")]
    public decimal LineTotal { get; set; } = 0;

    decimal _discount;
    [Description("Discount")]
    public decimal Discount
    {
        get => _discount;
        set
        {
            if (_discount == value) return;

            _discount = value;
            CalculateLineTotal();
        }

    }

    [Description("Product")]
    public ProductDto Product { get; set; } = null!;
    public string CacheKey => InvoiceLineCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => InvoiceLineCacheKey.Tags;

    public BehaviorSubject<int> ItemIdBehaviorSubject { get; set; } = new(0);
    public BehaviorSubject<decimal> LineTotalSubject { get; set; } = new(0);

    private void CalculateLineTotal()
    {
        LineTotal = Quantity * UnitPrice * (1 - Discount / 100);

        LineTotalSubject.OnNext(LineTotal); // Notify subscribers
    }

    public void Dispose()
    {
        ItemIdBehaviorSubject?.Dispose();
    }
}

public class AddEditInvoiceLineCommandHandler : IRequestHandler<AddEditInvoiceLineCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    public AddEditInvoiceLineCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(AddEditInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            //var item = await _context.InvoiceLines.FindAsync(request.Id, cancellationToken);
            //if (item == null)
            //{
            //    return await Result<int>.FailureAsync($"InvoiceLine with id: [{request.Id}] not found.");
            //}
            //InvoiceLineMapper.ApplyChangesFrom(request, item);
            //// raise a update domain event
            //item.AddDomainEvent(new InvoiceLineUpdatedEvent(item));
            //await _context.SaveChangesAsync(cancellationToken);
            //return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            //var item = InvoiceLineMapper.FromEditCommand(request);
            //// raise a create domain event
            //item.AddDomainEvent(new InvoiceLineCreatedEvent(item));
            //_context.InvoiceLines.Add(item);
            //await _context.SaveChangesAsync(cancellationToken);
            //return await Result<int>.SuccessAsync(item.Id);
        }


        return await Result<int>.SuccessAsync(0);
    }
}

