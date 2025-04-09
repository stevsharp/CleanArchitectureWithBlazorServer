
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Caching;

namespace CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Commands.Delete;

public class ConfirmPurchaseInvoiceCommand(int[] id) : ICacheInvalidatorRequest<Result<int>>
{
    public int[] Id { get; } = id;
    public string CacheKey => PurchaseInvoiceCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => PurchaseInvoiceCacheKey.Tags;
}

public class ConfirmPurchaseInvoiceCommandHandler(
    IApplicationDbContext context) : IRequestHandler<ConfirmPurchaseInvoiceCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<int>> Handle(ConfirmPurchaseInvoiceCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.PurchaseInvoices
            .Include(x => x.Items)  
            .Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        
        items.ForEach(x => x.Isfinalized = 1);

        foreach (var item in items)
        {
            foreach (var purchaseItem in item.Items)
            {
                var quantityDiff = purchaseItem.Quantity; // Deduct from stock

                var updatedCount = await _context.SubProducts
                .Where(x =>
                    x.Unit == purchaseItem.Unit &&
                    x.Color == purchaseItem.Color &&
                    x.ProductId == purchaseItem.ProductId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Stock, p => (p.Stock ?? 0) + quantityDiff)
                    .SetProperty(p => p.Price, p => purchaseItem.UnitPrice)
                    .SetProperty(p => p.LastModified, p => DateTime.UtcNow),
                    cancellationToken);

                if (updatedCount == 0)
                {
                    // Optional: log or handle missing SubProduct
                    throw new InvalidOperationException(
                        $"SubProduct not found for Unit={purchaseItem.Unit}, Color={purchaseItem.Color}, ProductId={purchaseItem.ProductId}");
                }
            }

            item.VATAmount += item.Items.Sum(x => x.VATAmount);
            item.TotalAmount += item.Items.Sum(x => x.TotalAmount);
        }

        var result = await _context.SaveChangesAsync(cancellationToken);

        return await Result<int>.SuccessAsync(result);
    }

}

