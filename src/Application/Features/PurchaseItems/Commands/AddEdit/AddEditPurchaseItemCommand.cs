
using System.Reactive.Linq;
using System.Reactive.Subjects;
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.Caching;
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.Mappers;


namespace CleanArchitecture.Blazor.Application.Features.PurchaseItems.Commands.AddEdit;

public class AddEditPurchaseItemCommand : ICacheInvalidatorRequest<Result<int>>
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

    [Description("ProductId")]
    public int ProductId { get; set; } = 0;

    [Description("Id")]
    public int Id { get; set; }
    [Description("Invoice id")]
    public int InvoiceId { get; set; }

    [Description("Item description")]
    public string? ItemDescription { get; set; }
    [Description("Quantity")]
    public int Quantity { get; set; }
    [Description("Unit")]
    public string? Unit { get; set; }
    [Description("Color")]
    public string? Color { get; set; }
    [Description("Unit price")]
    public decimal UnitPrice { get; set; }
    [Description("Vat percentage")]
    public decimal VATPercentage { get; set; } = 24;

    [Description("Ποσό ΦΠΑ")]
    public decimal VATAmount => UnitPrice * Quantity * VATPercentage / 100;

    [Description("Συνολικό ποσό")]
    public decimal TotalAmount => UnitPrice * Quantity + VATAmount;
    //[Description("Invoice")]
    //public PurchaseInvoiceDto Invoice {get;set;} 

    public string CacheKey => PurchaseItemCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => PurchaseItemCacheKey.Tags;
}

public class AddEditPurchaseItemCommandHandler(IApplicationDbContext context) : IRequestHandler<AddEditPurchaseItemCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<int>> Handle(AddEditPurchaseItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.InvoiceId <= 0)
                return await Result<int>.FailureAsync("Invalid invoice ID.");

            var purchaseInvoice = await _context.PurchaseInvoices
                .Include(x => x.Items)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == request.InvoiceId, cancellationToken);

            if (purchaseInvoice is null)
                return await Result<int>.FailureAsync($"Purchase invoice with ID [{request.InvoiceId}] not found.");

            // Determine if we're updating or adding
            var existingItem = purchaseInvoice.Items.FirstOrDefault(x => x.Id == request.Id);
            var quantityDiff = existingItem is null
                ? request.Quantity
                : request.Quantity - existingItem.Quantity;

            // Map and save purchase item
            if (existingItem is null)
            {

                //var updatedCount = 0;
                //// Try batch stock update
                //if (quantityDiff != 0)
                //{
                //    updatedCount = await _context.SubProducts
                //        .Where(x => x.Unit == request.Unit && x.Color == request.Color && x.ProductId == request.ProductId)
                //        .ExecuteUpdateAsync(setters => setters
                //            .SetProperty(p => p.Stock, p => (p.Stock ?? 0) + quantityDiff)
                //            .SetProperty(p => p.Price, p => request.UnitPrice),
                //            cancellationToken);
                //}

                //// Insert SubProduct if not found during batch update
                //if (updatedCount == 0)
                //{
                //    var newSubProduct = new SubProduct
                //    {
                //        Unit = request.Unit,
                //        Color = request.Color,
                //        ProductId = request.ProductId,
                //        Price = request.UnitPrice,
                //        Stock = quantityDiff
                //    };

                //    _context.SubProducts.Add(newSubProduct);
                //}

                var newItem = PurchaseItemMapper.FromEditCommand(request);
                purchaseInvoice.Items.Add(newItem);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(newItem.Id);
            }
            else
            {

                quantityDiff = request.Quantity - existingItem.Quantity;

                // Update the item itself
                PurchaseItemMapper.ApplyChangesFrom(request, existingItem);

                // Only adjust stock if there's a quantity change
                //if (quantityDiff != 0)
                //{
                //    var updatedCount = await _context.SubProducts
                //        .Where(x => x.Unit == request.Unit && x.Color == request.Color && x.ProductId == request.ProductId)
                //        .ExecuteUpdateAsync(setters => setters
                //            .SetProperty(p => p.Stock, p => (p.Stock ?? 0) + quantityDiff)
                //            .SetProperty(p => p.Price, p => request.UnitPrice),
                //            cancellationToken);

                //    // Optional: log if the SubProduct was not found (should never happen during update)
                //    if (updatedCount == 0)
                //    {
                //        return await Result<int>.FailureAsync("Associated SubProduct not found for stock update.");
                //    }
                //}

                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(existingItem.Id);

                //PurchaseItemMapper.ApplyChangesFrom(request, existingItem);
                //await _context.SaveChangesAsync(cancellationToken);
                //return await Result<int>.SuccessAsync(existingItem.Id);
            }


            //if (request.InvoiceId <= 0)
            //    return await Result<int>.FailureAsync("Invalid invoice ID.");

            //var purchaseInvoice = await _context.PurchaseInvoices
            //    .Include(x => x.Items)
            //    .AsSingleQuery()
            //    .FirstOrDefaultAsync(x => x.Id == request.InvoiceId, cancellationToken);

            //if (purchaseInvoice is null)
            //    return await Result<int>.FailureAsync($"Purchase invoice with ID [{request.InvoiceId}] not found.");

            //// Extract method to get or create SubProduct
            //async Task<SubProduct> GetOrCreateSubProductAsync(CancellationToken ct)
            //{
            //    var productItem = await _context.SubProducts
            //        .Where(x => x.Unit == request.Unit 
            //        && x.Color == request.Color 
            //        && x.ProductId == request.ProductId)
            //        .FirstOrDefaultAsync(ct);

            //    if (productItem is not null)
            //        return productItem;

            //    var newSubProduct = new SubProduct
            //    {
            //        Unit = request.Unit,
            //        Color = request.Color,
            //        ProductId = request.ProductId,
            //        Price = request.UnitPrice,
            //        Stock = 0
            //    };

            //    _context.SubProducts.Add(newSubProduct);
            //    return newSubProduct;
            //}

            //var item = purchaseInvoice.Items.FirstOrDefault(x => x.Id == request.Id);
            //var subProduct = await GetOrCreateSubProductAsync(cancellationToken);

            //if (item is null)
            //{
            //    // New item
            //    var newItem = PurchaseItemMapper.FromEditCommand(request);
            //    purchaseInvoice.Items.Add(newItem);

            //    int updatedCount = await _context.SubProducts
            //        .Where(x => x.Unit == request.Unit && x.Color == request.Color && x.ProductId == request.ProductId)
            //        .ExecuteUpdateAsync(setters => setters
            //            .SetProperty(p => p.Stock, p => p.Stock + newItem.Quantity),
            //            cancellationToken);

            //    _context.SubProducts.Update(subProduct); 

            //    await _context.SaveChangesAsync(cancellationToken);

            //    return await Result<int>.SuccessAsync(newItem.Id);
            //}
            //else
            //{
            //    // Update existing item
            //    var quantityDiff = request.Quantity - item.Quantity;

            //    PurchaseItemMapper.ApplyChangesFrom(request, item);
            //    subProduct.Stock += quantityDiff;

            //    await _context.SaveChangesAsync(cancellationToken);
            //    return await Result<int>.SuccessAsync(item.Id);
            //}
            //else
            //{
            //    var item = PurchaseItemMapper.FromEditCommand(request);
            //    // raise a create domain event
            //    //item.AddDomainEvent(new PurchaseItemCreatedEvent(item));
            //    _context.PurchaseItems.Add(item);
            //    await _context.SaveChangesAsync(cancellationToken);
            //    return await Result<int>.SuccessAsync(item.Id);
            //}
        }
        catch (Exception ex)
        {
            var message = $"Error saving purchase item: {request}";
            throw;
        }

    }
}

