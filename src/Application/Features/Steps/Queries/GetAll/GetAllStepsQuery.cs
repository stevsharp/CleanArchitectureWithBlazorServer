

using CleanArchitecture.Blazor.Application.Features.Steps.DTOs;
using CleanArchitecture.Blazor.Application.Features.Steps.Mappers;
using CleanArchitecture.Blazor.Application.Features.Steps.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Steps.Queries.GetAll;

public class GetAllStepsQuery : ICacheableRequest<IEnumerable<StepDto>>
{
   public string CacheKey => StepCacheKey.GetAllCacheKey;
   public IEnumerable<string>? Tags => StepCacheKey.Tags;
}

public class GetAllStepsQueryHandler :
     IRequestHandler<GetAllStepsQuery, IEnumerable<StepDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllStepsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StepDto>> Handle(GetAllStepsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Steps.ProjectTo()
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);
        return data;
    }
}


