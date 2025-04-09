

using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.DTOs;
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Caching;
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Mappers;
using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Queries.Pagination;

public class PurchaseInvoicesWithPaginationQuery : PurchaseInvoiceAdvancedFilter, ICacheableRequest<PaginatedData<PurchaseInvoiceDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}-{LocalTimezoneOffset.TotalHours}, Search:{Keyword}, {InvoiceNumber} {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => PurchaseInvoiceCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => PurchaseInvoiceCacheKey.Tags;
    public PurchaseInvoiceAdvancedSpecification Specification => new(this);
}
    
public class PurchaseInvoicesWithPaginationQueryHandler(
         IApplicationDbContext context) :
         IRequestHandler<PurchaseInvoicesWithPaginationQuery, PaginatedData<PurchaseInvoiceDto>>
{
        private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedData<PurchaseInvoiceDto>> Handle(PurchaseInvoicesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _context.PurchaseInvoices
                                                        .Include(x => x.Supplier)
                                                        .Include(x => x.Items)
                                                        .AsSplitQuery()
                                                        .OrderBy($"{request.OrderBy} {request.SortDirection}")
                                                        .ProjectToPaginatedDataAsync(request.Specification,
                                                                                     request.PageNumber,
                                                                                     request.PageSize,
                                                                                     PurchaseInvoiceMapper.ToDto,
                                                                                     cancellationToken);
                return data;
            }
            catch (Exception ex)
            {
                var message = $"Error loading purchase invoices with pagination query: {request}";
                throw;
            }

        }
}