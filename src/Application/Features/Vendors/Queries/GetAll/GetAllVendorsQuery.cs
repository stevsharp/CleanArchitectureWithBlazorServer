
#nullable enable
#nullable disable warnings

using CleanArchitecture.Blazor.Application.Features.Vendors.DTOs;
using CleanArchitecture.Blazor.Application.Features.Vendors.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Vendors.Queries.GetAll;

public class GetAllVendorsQuery : ICacheableRequest<IEnumerable<VendorDto>>
{
   public string CacheKey => VendorCacheKey.GetAllCacheKey;
   public IEnumerable<string>? Tags => VendorCacheKey.Tags;
}

public class GetAllVendorsQueryHandler :
     IRequestHandler<GetAllVendorsQuery, IEnumerable<VendorDto>>
{
    private readonly IApplicationDbContextFactory _dbContextFactory;
    private readonly IMapper _mapper;
    public GetAllVendorsQueryHandler(
        IMapper mapper,
        IApplicationDbContextFactory dbContextFactory)
    {
        _mapper = mapper;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<VendorDto>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
    {
        await using var _context = await _dbContextFactory.CreateAsync(cancellationToken);
        var data = await _context.Vendors.ProjectTo<VendorDto>(_mapper.ConfigurationProvider)
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);
        return data;
    }
}


