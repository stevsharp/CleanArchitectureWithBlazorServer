
using CleanArchitecture.Blazor.Application.Features.Steps.DTOs;
using CleanArchitecture.Blazor.Application.Features.Steps.Caching;
using CleanArchitecture.Blazor.Application.Features.Steps.Mappers;
using CleanArchitecture.Blazor.Application.Features.Steps.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Steps.Queries.Pagination;

public class StepsWithPaginationQuery : StepAdvancedFilter, ICacheableRequest<PaginatedData<StepDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}-{LocalTimezoneOffset.TotalHours}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => StepCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => StepCacheKey.Tags;
    public StepAdvancedSpecification Specification => new StepAdvancedSpecification(this);
}
    
public class StepsWithPaginationQueryHandler :
         IRequestHandler<StepsWithPaginationQuery, PaginatedData<StepDto>>
{
        private readonly IApplicationDbContext _context;

        public StepsWithPaginationQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedData<StepDto>> Handle(StepsWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.Steps.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                                   .ProjectToPaginatedDataAsync(request.Specification, 
                                                                                request.PageNumber, 
                                                                                request.PageSize, 
                                                                                StepMapper.ToDto, 
                                                                                cancellationToken);
            return data;
        }
}