
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.Caching;
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.PurchaseItems.Commands.AddEdit;

public class AddEditPurchaseItemCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Invoice id")]
    public int InvoiceId { get; set; }
    [Description("Item code")]
    public string? ItemCode { get; set; }
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
    public decimal VATPercentage { get; set; }

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
            if (request.InvoiceId > 0)
            {
                var purchaseInvoice = await _context.PurchaseInvoices
                    .Include(x => x.Items)
                    .AsSingleQuery()
                    .FirstOrDefaultAsync(x => x.Id == request.InvoiceId, cancellationToken);

                //var item = await _context.PurchaseItems.FindAsync(request.Id, cancellationToken);
                if (purchaseInvoice is null)
                {
                    return await Result<int>.FailureAsync($"Purchase with id: [{request.Id}] not found.");
                }


                var items = purchaseInvoice.Items.ToList();
                if (!items.Any())
                {
                    var item = PurchaseItemMapper.FromEditCommand(request);
                    items.Add(item);

                    await _context.SaveChangesAsync(cancellationToken);
                    return await Result<int>.SuccessAsync(item.Id);
                }

                var existingItem = items.FirstOrDefault(x => x.Id == request.Id);
                if (existingItem is not null)
                {
                    PurchaseItemMapper.ApplyChangesFrom(request, existingItem);

                    await _context.SaveChangesAsync(cancellationToken);
                    return await Result<int>.SuccessAsync(existingItem.Id);
                }

                // raise a update domain event
                //item.AddDomainEvent(new PurchaseItemUpdatedEvent(item));

            }
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

        return await Result<int>.SuccessAsync(0);
    }
}

