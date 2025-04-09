
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Caching;


namespace CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Commands.Delete;

public class DeletePurchaseInvoiceCommand:  ICacheInvalidatorRequest<Result<int>>
{
  public int[] Id {  get; }
  public string CacheKey => PurchaseInvoiceCacheKey.GetAllCacheKey;
  public IEnumerable<string>? Tags => PurchaseInvoiceCacheKey.Tags;
  public DeletePurchaseInvoiceCommand(int[] id)
  {
    Id = id;
  }
}

public class DeletePurchaseInvoiceCommandHandler : IRequestHandler<DeletePurchaseInvoiceCommand, Result<int>>

{
    private readonly IApplicationDbContext _context;
    public DeletePurchaseInvoiceCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(DeletePurchaseInvoiceCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.PurchaseInvoices.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        foreach (var item in items)
        {
		    // raise a delete domain event
			//item.AddDomainEvent(new PurchaseInvoiceDeletedEvent(item));
            _context.PurchaseInvoices.Remove(item);
        }
        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }

}

