
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.DTOs;
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Mappers;
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Caching;

namespace CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Queries.GetAll;

public class GetAllPurchaseInvoicesQuery : ICacheableRequest<IEnumerable<PurchaseInvoiceDto>>
{
   public string CacheKey => PurchaseInvoiceCacheKey.GetAllCacheKey;
   public IEnumerable<string>? Tags => PurchaseInvoiceCacheKey.Tags;
}

public class GetAllPurchaseInvoicesQueryHandler :
     IRequestHandler<GetAllPurchaseInvoicesQuery, IEnumerable<PurchaseInvoiceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllPurchaseInvoicesQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PurchaseInvoiceDto>> Handle(GetAllPurchaseInvoicesQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.PurchaseInvoices.ProjectToDto()
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);
        return data;
    }
}


