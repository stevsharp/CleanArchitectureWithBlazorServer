
using CleanArchitecture.Blazor.Application.Features.Steps.DTOs;
using CleanArchitecture.Blazor.Application.Features.Steps.Caching;
using CleanArchitecture.Blazor.Application.Features.Steps.Mappers;
using CleanArchitecture.Blazor.Application.Features.Steps.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Steps.Queries.GetById;

public class GetStepByIdQuery : ICacheableRequest<Result<StepDto>>
{
   public required int Id { get; set; }
   public string CacheKey => StepCacheKey.GetByIdCacheKey($"{Id}");
   public IEnumerable<string>? Tags => StepCacheKey.Tags;
}

public class GetStepByIdQueryHandler :
     IRequestHandler<GetStepByIdQuery, Result<StepDto>>
{
    private readonly IApplicationDbContext _context;

    public GetStepByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<StepDto>> Handle(GetStepByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Steps.ApplySpecification(new StepByIdSpecification(request.Id))
                                                .ProjectTo()
                                                .FirstAsync(cancellationToken);
        return await Result<StepDto>.SuccessAsync(data);
    }
}
