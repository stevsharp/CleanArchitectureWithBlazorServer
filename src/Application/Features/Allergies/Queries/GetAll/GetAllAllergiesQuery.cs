// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Allergies.DTOs;
using CleanArchitecture.Blazor.Application.Features.Allergies.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Allergies.Queries.GetAll;

public class GetAllAllergiesQuery : ICacheableRequest<IEnumerable<AllergyDto>>
{
   public string CacheKey => AllergyCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => AllergyCacheKey.MemoryCacheEntryOptions;
}

public class GetAllAllergiesQueryHandler :
     IRequestHandler<GetAllAllergiesQuery, IEnumerable<AllergyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllAllergiesQueryHandler> _localizer;

    public GetAllAllergiesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllAllergiesQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<AllergyDto>> Handle(GetAllAllergiesQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Allergies
                     .ProjectTo<AllergyDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


