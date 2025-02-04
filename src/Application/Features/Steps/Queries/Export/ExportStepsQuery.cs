
using CleanArchitecture.Blazor.Application.Features.Steps.DTOs;
using CleanArchitecture.Blazor.Application.Features.Steps.Mappers;
using CleanArchitecture.Blazor.Application.Features.Steps.Caching;
using CleanArchitecture.Blazor.Application.Features.Steps.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Steps.Queries.Export;

public class ExportStepsQuery : StepAdvancedFilter, ICacheableRequest<Result<byte[]>>
{
      public StepAdvancedSpecification Specification => new StepAdvancedSpecification(this);
      public IEnumerable<string>? Tags => StepCacheKey.Tags;
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}-{LocalTimezoneOffset.TotalHours}, Search:{Keyword}, {OrderBy}, {SortDirection}";
    }
    public string CacheKey => StepCacheKey.GetExportCacheKey($"{this}");
}
    
public class ExportStepsQueryHandler :
         IRequestHandler<ExportStepsQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportStepsQueryHandler> _localizer;
        private readonly StepDto _dto = new();
        public ExportStepsQueryHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ExportStepsQueryHandler> localizer
            )
        {
            _context = context;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportStepsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Steps.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo()
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<StepDto, object?>>()
                {
                    // TODO: Define the fields that should be exported, for example:
                    {_localizer[_dto.GetMemberDescription(x=>x.Name)],item => item.Name}, 
{_localizer[_dto.GetMemberDescription(x=>x.InvoiceId)],item => item.InvoiceId}, 
{_localizer[_dto.GetMemberDescription(x=>x.IsCompleted)],item => item.IsCompleted}, 
{_localizer[_dto.GetMemberDescription(x=>x.StepOrder)],item => item.StepOrder}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
}
