using CleanArchitecture.Blazor.Application.Features.Steps.Caching;
using CleanArchitecture.Blazor.Application.Features.Steps.DTOs;
using CleanArchitecture.Blazor.Application.Features.Steps.Mappers;
using CleanArchitecture.Blazor.Application.Features.Steps.Specifications;

public class AllStepsWithPaginationQuery : StepAdvancedFilter, ICacheableRequest<PaginatedData<StepDto>>
{
    public int InvoiceId { get; set; }
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}-{LocalTimezoneOffset.TotalHours}, " +
            $"Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => StepCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => StepCacheKey.Tags;
    public StepAdvancedSpecification Specification => new StepAdvancedSpecification(this);
}

public class AllStepsWithPaginationQueryHandler(
         IApplicationDbContext context) :
         IRequestHandler<AllStepsWithPaginationQuery, PaginatedData<StepDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<PaginatedData<StepDto>> Handle(AllStepsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _context.Steps
                .ApplySpecification(new AllStepByIdSpecification())
                .Include(x => x.Comments)
                .Include(x => x.Invoice)
                .OrderBy($"{request.OrderBy} {request.SortDirection}")
                                          .ProjectToPaginatedDataAsync(request.Specification,
                                                                       request.PageNumber,
                                                                       request.PageSize,
                                                                       StepMapper.ToDto,
                                                                       cancellationToken);
            return data;
        }
        catch (Exception ex)
        {
            var msg = $"Error: {ex.Message} at {ex.StackTrace}";
            throw;
        }

    }
}