using CleanArchitecture.Blazor.Application.Features.Steps.Caching;
using CleanArchitecture.Blazor.Application.Features.Steps.DTOs;
using CleanArchitecture.Blazor.Application.Features.Steps.Mappers;
using CleanArchitecture.Blazor.Application.Features.Steps.Specifications;

public class AllStepsWithPaginationQuery : StepAdvancedFilter, ICacheableRequest<PaginatedData<StepDto>>
{
    public string Role { get; set; } = string.Empty;
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
            var roleToStepOrder = new Dictionary<string, int>
            {
                { "WareHouse", 2 },
                { "Print", 3 }
            };

            var stepOrder = roleToStepOrder.TryGetValue(request.Role, out var order) ? order : 0;


            var data = await _context.Steps
                .ApplySpecification(new AllStepByIdSpecificationWithStep(stepOrder , 1))
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


//SELECT * 
//FROM [dbo].[Steps] AS s1
//WHERE s1.StepOrder = 2 -- 'Create Product' step
//  AND s1.InvoiceId = 11
//  AND EXISTS (
//      SELECT 1
//      FROM [dbo].[Steps] AS s2
//      WHERE s2.StepOrder = 1 -- 'Make Deposit' step
//        AND s2.IsCompleted = 1
//        AND s2.InvoiceId = s1.InvoiceId
//  )SELECT * 
//FROM [dbo].[Steps] AS s1
//WHERE s1.StepOrder = 2 -- 'Create Product' step
//  AND s1.InvoiceId = 11
//  AND EXISTS (
//      SELECT 1
//      FROM [dbo].[Steps] AS s2
//      WHERE s2.StepOrder = 1 -- 'Make Deposit' step
//        AND s2.IsCompleted = 1
//        AND s2.InvoiceId = s1.InvoiceId
//  )