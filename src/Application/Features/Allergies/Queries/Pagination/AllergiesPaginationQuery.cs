// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Allergies.DTOs;
using CleanArchitecture.Blazor.Application.Features.Allergies.Caching;
using CleanArchitecture.Blazor.Application.Features.Allergies.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Allergies.Queries.Pagination;

public class AllergiesWithPaginationQuery : AllergyAdvancedFilter, ICacheableRequest<PaginatedData<AllergyDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}-{LocalTimezoneOffset.TotalHours}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => AllergyCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => AllergyCacheKey.MemoryCacheEntryOptions;
    public AllergyAdvancedSpecification Specification => new AllergyAdvancedSpecification(this);
}
    
public class AllergiesWithPaginationQueryHandler :
         IRequestHandler<AllergiesWithPaginationQuery, PaginatedData<AllergyDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AllergiesWithPaginationQueryHandler> _localizer;

        public AllergiesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<AllergiesWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<AllergyDto>> Handle(AllergiesWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.Allergies.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<Allergy, AllergyDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}