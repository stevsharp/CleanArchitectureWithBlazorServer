

using CleanArchitecture.Blazor.Application.Features.SubProducts.DTOs;
using CleanArchitecture.Blazor.Application.Features.SubProducts.Caching;
using CleanArchitecture.Blazor.Application.Features.SubProducts.Mappers;
using CleanArchitecture.Blazor.Application.Features.SubProducts.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.SubProducts.Queries.Pagination;

public class SubProductsWithPaginationQuery : SubProductAdvancedFilter, ICacheableRequest<PaginatedData<SubProductDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}-{LocalTimezoneOffset.TotalHours}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public int ProductId { get;set; }
    public string CacheKey => SubProductCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => SubProductCacheKey.Tags;
    public SubProductAdvancedSpecification Specification => new SubProductAdvancedSpecification(this);
}
    
public class SubProductsWithPaginationQueryHandler(
    IApplicationDbContext context) :
         IRequestHandler<SubProductsWithPaginationQuery, PaginatedData<SubProductDto>>
{
        private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedData<SubProductDto>> Handle(SubProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var data = await _context.SubProducts.Include(x=>x.Product)
                            .Where(x => x.ProductId == request.ProductId)
                                                 .OrderBy($"{request.OrderBy} {request.SortDirection}")
                                                        .ProjectToPaginatedDataAsync(request.Specification,
                                                                                     request.PageNumber,
                                                                                     request.PageSize,
                                                                                     SubProductMapper.ToDto,
                                                                                     cancellationToken);
                return data;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw;
            }

        }
}