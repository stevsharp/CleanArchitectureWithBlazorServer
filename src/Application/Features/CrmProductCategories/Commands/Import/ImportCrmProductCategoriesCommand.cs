﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under one or more agreements.
//     The .NET Foundation licenses this file to you under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-02
//     Last Modified: 2024-12-02
//     Description: 
//       This file defines the command, handler, and associated logic for importing 
//       crmproductcategorys from an Excel file into the CleanArchitecture.Blazor application. 
//       The import process supports validating data and ensuring no duplicates are 
//       inserted. Additionally, a command for creating a crmproductcategory template file is provided 
//       to facilitate bulk data entry for end users.
//     
//     Documentation:
//       https://docs.cleanarchitectureblazor.com/features/crmproductcategory
// </auto-generated>
//------------------------------------------------------------------------------

// Usage:
// - Use `ImportCrmProductCategorysCommand` to import crmproductcategorys from an Excel file, ensuring proper validation
//   and avoiding duplicates.
// - Use `CreateCrmProductCategorysTemplateCommand` to generate an Excel template for entering crmproductcategory data 
//   that can be later imported using the import command.

using CleanArchitecture.Blazor.Application.Features.CrmProductCategories.DTOs;
using CleanArchitecture.Blazor.Application.Features.CrmProductCategories.Caching;
using CleanArchitecture.Blazor.Application.Features.CrmProductCategories.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.CrmProductCategories.Commands.Import;

    public class ImportCrmProductCategoriesCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => CrmProductCategoryCacheKey.GetAllCacheKey;
        public IEnumerable<string>? Tags => CrmProductCategoryCacheKey.Tags;
        public ImportCrmProductCategoriesCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateCrmProductCategoriesTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportCrmProductCategoriesCommandHandler : 
                 IRequestHandler<CreateCrmProductCategoriesTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportCrmProductCategoriesCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStringLocalizer<ImportCrmProductCategoriesCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly CrmProductCategoryDto _dto = new();

        public ImportCrmProductCategoriesCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportCrmProductCategoriesCommandHandler> localizer)
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportCrmProductCategoriesCommand request, CancellationToken cancellationToken)
        {

           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, CrmProductCategoryDto, object?>>
            {
                { _localizer[_dto.GetMemberDescription(x=>x.CategoryName)], (row, item) => item.CategoryName = row[_localizer[_dto.GetMemberDescription(x=>x.CategoryName)]].ToString() }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.CrmProductCategories.AnyAsync(x => x.CategoryName == dto.CategoryName, cancellationToken);
                    if (!exists)
                    {
                        var item = CrmProductCategoryMapper.FromDto(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new CrmProductCategoryCreatedEvent(item));
                        await _context.CrmProductCategories.AddAsync(item, cancellationToken);
                    }
                 }
                 await _context.SaveChangesAsync(cancellationToken);
                 return await Result<int>.SuccessAsync(result.Data.Count());
           }
           else
           {
               return await Result<int>.FailureAsync(result.Errors);
           }
        }
        public async Task<Result<byte[]>> Handle(CreateCrmProductCategoriesTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportCrmProductCategoriesCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.CategoryName)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

