﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under one or more agreements.
//     The .NET Foundation licenses this file to you under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-06
//     Last Modified: 2024-12-06
//     Description: 
//       This file defines the command, handler, and associated logic for importing 
//       suppliers from an Excel file into the CleanArchitecture.Blazor application. 
//       The import process supports validating data and ensuring no duplicates are 
//       inserted. Additionally, a command for creating a supplier template file is provided 
//       to facilitate bulk data entry for end users.
//     
//     Documentation:
//       https://docs.cleanarchitectureblazor.com/features/supplier
// </auto-generated>
//------------------------------------------------------------------------------

// Usage:
// - Use `ImportSuppliersCommand` to import suppliers from an Excel file, ensuring proper validation
//   and avoiding duplicates.
// - Use `CreateSuppliersTemplateCommand` to generate an Excel template for entering supplier data 
//   that can be later imported using the import command.

using CleanArchitecture.Blazor.Application.Features.Suppliers.DTOs;
using CleanArchitecture.Blazor.Application.Features.Suppliers.Caching;
using CleanArchitecture.Blazor.Application.Features.Suppliers.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.Suppliers.Commands.Import;

    public class ImportSuppliersCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => SupplierCacheKey.GetAllCacheKey;
        public IEnumerable<string>? Tags => SupplierCacheKey.Tags;
        public ImportSuppliersCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateSuppliersTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportSuppliersCommandHandler : 
                 IRequestHandler<CreateSuppliersTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportSuppliersCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStringLocalizer<ImportSuppliersCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly SupplierDto _dto = new();

        public ImportSuppliersCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportSuppliersCommandHandler> localizer)
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportSuppliersCommand request, CancellationToken cancellationToken)
        {

           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, SupplierDto, object?>>
            {
                { _localizer[_dto.GetMemberDescription(x=>x.Name)], (row, item) => item.Name = row[_localizer[_dto.GetMemberDescription(x=>x.Name)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Address)], (row, item) => item.Address = row[_localizer[_dto.GetMemberDescription(x=>x.Address)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Phone)], (row, item) => item.Phone = row[_localizer[_dto.GetMemberDescription(x=>x.Phone)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Email)], (row, item) => item.Email = row[_localizer[_dto.GetMemberDescription(x=>x.Email)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.VAT)], (row, item) => item.VAT = row[_localizer[_dto.GetMemberDescription(x=>x.VAT)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Country)], (row, item) => item.Country = row[_localizer[_dto.GetMemberDescription(x=>x.Country)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.CreatedAt)], (row, item) => item.CreatedAt =DateTime.Parse(row[_localizer[_dto.GetMemberDescription(x=>x.CreatedAt)]].ToString()) }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.Suppliers.AnyAsync(x => x.Name == dto.Name, cancellationToken);
                    if (!exists)
                    {
                        var item = SupplierMapper.FromDto(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new SupplierCreatedEvent(item));
                        await _context.Suppliers.AddAsync(item, cancellationToken);
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
        public async Task<Result<byte[]>> Handle(CreateSuppliersTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportSuppliersCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.Name)], 
_localizer[_dto.GetMemberDescription(x=>x.Address)], 
_localizer[_dto.GetMemberDescription(x=>x.Phone)], 
_localizer[_dto.GetMemberDescription(x=>x.Email)], 
_localizer[_dto.GetMemberDescription(x=>x.VAT)], 
_localizer[_dto.GetMemberDescription(x=>x.Country)], 
_localizer[_dto.GetMemberDescription(x=>x.CreatedAt)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

