﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-06
//     Last Modified: 2024-12-06
//     Description: 
//       Defines a query to export offer data to an Excel file. This query 
//       applies advanced filtering options and generates an Excel file with 
//       the specified offer details.
// </auto-generated>
//------------------------------------------------------------------------------

using CleanArchitecture.Blazor.Application.Features.Offers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Offers.Mappers;
using CleanArchitecture.Blazor.Application.Features.Offers.Caching;
using CleanArchitecture.Blazor.Application.Features.Offers.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Offers.Queries.Export;

public class ExportOffersQuery : OfferAdvancedFilter, ICacheableRequest<Result<byte[]>>
{
      public OfferAdvancedSpecification Specification => new OfferAdvancedSpecification(this);
      public IEnumerable<string>? Tags => OfferCacheKey.Tags;
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}-{LocalTimezoneOffset.TotalHours}, Search:{Keyword}, {OrderBy}, {SortDirection}";
    }
    public string CacheKey => OfferCacheKey.GetExportCacheKey($"{this}");
}
    
public class ExportOffersQueryHandler :
         IRequestHandler<ExportOffersQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportOffersQueryHandler> _localizer;
        private readonly OfferDto _dto = new();
        public ExportOffersQueryHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ExportOffersQueryHandler> localizer
            )
        {
            _context = context;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportOffersQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Offers.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo()
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<OfferDto, object?>>()
                {
                    // TODO: Define the fields that should be exported, for example:
                    {_localizer[_dto.GetMemberDescription(x=>x.CustomerId)],item => item.CustomerId}, 
{_localizer[_dto.GetMemberDescription(x=>x.OfferDate)],item => item.OfferDate}, 
{_localizer[_dto.GetMemberDescription(x=>x.TotalAmount)],item => item.TotalAmount}, 
{_localizer[_dto.GetMemberDescription(x=>x.Status)],item => item.Status}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
}
