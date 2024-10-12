// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Allergies.DTOs;
using CleanArchitecture.Blazor.Application.Features.Allergies.Specifications;
using CleanArchitecture.Blazor.Application.Features.Allergies.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.Allergies.Queries.Export;

public class ExportAllergiesQuery : AllergyAdvancedFilter, IRequest<Result<byte[]>>
{
      public AllergyAdvancedSpecification Specification => new AllergyAdvancedSpecification(this);
}
    
public class ExportAllergiesQueryHandler :
         IRequestHandler<ExportAllergiesQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportAllergiesQueryHandler> _localizer;
        private readonly AllergyDto _dto = new();
        public ExportAllergiesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportAllergiesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportAllergiesQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Allergies.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<AllergyDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<AllergyDto, object?>>()
                {
                    // TODO: Define the fields that should be exported, for example:
                    {_localizer[_dto.GetMemberDescription(x=>x.AllergyType)],item => item.AllergyType}, 
{_localizer[_dto.GetMemberDescription(x=>x.Comments)],item => item.Comments}, 
{_localizer[_dto.GetMemberDescription(x=>x.PatientId)],item => item.PatientId}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
}
