// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Allergies.DTOs;
using CleanArchitecture.Blazor.Application.Features.Allergies.Caching;
using CleanArchitecture.Blazor.Application.Features.Allergies.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Allergies.Queries.GetById;

public class GetAllergyByIdQuery : ICacheableRequest<Result<AllergyDto>>
{
   public required int Id { get; set; }
   public string CacheKey => AllergyCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => AllergyCacheKey.MemoryCacheEntryOptions;
}

public class GetAllergyByIdQueryHandler :
     IRequestHandler<GetAllergyByIdQuery, Result<AllergyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllergyByIdQueryHandler> _localizer;

    public GetAllergyByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllergyByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<Result<AllergyDto>> Handle(GetAllergyByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Allergies.ApplySpecification(new AllergyByIdSpecification(request.Id))
                     .ProjectTo<AllergyDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken);
        return await Result<AllergyDto>.SuccessAsync(data);
    }
}
