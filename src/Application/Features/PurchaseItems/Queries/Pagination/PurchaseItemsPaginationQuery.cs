

using CleanArchitecture.Blazor.Application.Features.PurchaseItems.DTOs;
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.Caching;
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.Mappers;
using CleanArchitecture.Blazor.Application.Features.PurchaseItems.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.PurchaseItems.Queries.Pagination;

public class PurchaseItemsWithPaginationQuery : PurchaseItemAdvancedFilter, ICacheableRequest<PaginatedData<PurchaseItemDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}-{LocalTimezoneOffset.TotalHours}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }

    public string CacheKey => PurchaseItemCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => PurchaseItemCacheKey.Tags;
    public PurchaseItemAdvancedSpecification Specification => new PurchaseItemAdvancedSpecification(this);
}

public class PurchaseItemsWithPaginationQueryHandler(
    IApplicationDbContext context) :
         IRequestHandler<PurchaseItemsWithPaginationQuery, PaginatedData<PurchaseItemDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedData<PurchaseItemDto>> Handle(PurchaseItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _context.PurchaseItems.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                                    .ProjectToPaginatedDataAsync(request.Specification,
                                                                                 request.PageNumber,
                                                                                 request.PageSize,
                                                                                 PurchaseItemMapper.ToDto,
                                                                                 cancellationToken);
            return data;
        }
        catch (Exception ex)
        {
            var message = $"Error loading purchase items with pagination query: {request}";
            throw;
        }

    }
}