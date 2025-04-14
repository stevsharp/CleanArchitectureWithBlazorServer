

using CleanArchitecture.Blazor.Application.Features.SubProducts.DTOs;
using CleanArchitecture.Blazor.Application.Features.SubProducts.Caching;
using CleanArchitecture.Blazor.Application.Features.SubProducts.Mappers;
using CleanArchitecture.Blazor.Application.Features.SubProducts.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.SubProducts.Queries.GetById;

public class GetSubProductByIdQuery : ICacheableRequest<Result<SubProductDto>>
{
   public required int Id { get; set; }
   public string CacheKey => SubProductCacheKey.GetByIdCacheKey($"{Id}");
   public IEnumerable<string>? Tags => SubProductCacheKey.Tags;
}

public class GetSubProductByIdQueryHandler :
     IRequestHandler<GetSubProductByIdQuery, Result<SubProductDto>>
{
    private readonly IApplicationDbContext _context;

    public GetSubProductByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<SubProductDto>> Handle(GetSubProductByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.SubProducts.ApplySpecification(new SubProductByIdSpecification(request.Id))
                                                .ProjectTo()
                                                .FirstAsync(cancellationToken);
        return await Result<SubProductDto>.SuccessAsync(data);
    }
}

public class GetSubProductByProductIdQuery : ICacheableRequest<Result<IEnumerable<SubProductDto>>>
{
    public required int ProductId { get; set; }
    public string CacheKey => SubProductCacheKey.GetByIdCacheKey($"{ProductId}");
    public IEnumerable<string>? Tags => SubProductCacheKey.Tags;
}

public class GetSubProductByProductByIdHandler(IApplicationDbContext context) : IRequestHandler<GetSubProductByProductIdQuery, Result<IEnumerable<SubProductDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<IEnumerable<SubProductDto>>> Handle(GetSubProductByProductIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.SubProducts.ApplySpecification(new SubProductByProductIdSpecification(request.ProductId))
                                                .ProjectTo()
                                                .ToListAsync(cancellationToken);

        return await Result<IEnumerable<SubProductDto>>.SuccessAsync(data);
    }
}
