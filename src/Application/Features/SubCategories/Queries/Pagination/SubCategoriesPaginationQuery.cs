
using CleanArchitecture.Blazor.Application.Features.SubCategories.DTOs;
using CleanArchitecture.Blazor.Application.Features.SubCategories.Caching;
using CleanArchitecture.Blazor.Application.Features.SubCategories.Mappers;
using CleanArchitecture.Blazor.Application.Features.SubCategories.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.SubCategories.Queries.Pagination;

public class SubCategoriesWithPaginationQuery : SubCategoryAdvancedFilter, ICacheableRequest<PaginatedData<SubCategoryDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}-{LocalTimezoneOffset.TotalHours}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => SubCategoryCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => SubCategoryCacheKey.Tags;
    public SubCategoryAdvancedSpecification Specification => new SubCategoryAdvancedSpecification(this);
}
    
public class SubCategoriesWithPaginationQueryHandler(
    IApplicationDbContext context) :
         IRequestHandler<SubCategoriesWithPaginationQuery, PaginatedData<SubCategoryDto>>
{
        private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedData<SubCategoryDto>> Handle(SubCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.SubCategories
                .AsSplitQuery()
                .OrderBy($"{request.OrderBy} {request.SortDirection}")
                                                       .ProjectToPaginatedDataAsync(request.Specification, 
                                                                                    request.PageNumber, 
                                                                                    request.PageSize, 
                                                                                    SubCategoryMapper.ToDto, 
                                                                                    cancellationToken);
            return data;
        }
}