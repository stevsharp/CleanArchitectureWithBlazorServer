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
//       This file defines the command for adding or editing a supplier entity,
//       including validation and mapping operations. It handles domain events
//       and cache invalidation for updated or newly created supplier.
//     
//     Documentation:
//       https://docs.cleanarchitectureblazor.com/features/supplier
// </auto-generated>
//------------------------------------------------------------------------------

// Usage:
// This command can be used to add a new supplier or edit an existing one.
// It handles caching logic and domain event raising automatically.


using CleanArchitecture.Blazor.Application.Features.Suppliers.Caching;
using CleanArchitecture.Blazor.Application.Features.Suppliers.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.Suppliers.Commands.AddEdit;

public class AddEditSupplierCommand: ICacheInvalidatorRequest<Result<int>>
{
      [Description("Id")]
      public int Id { get; set; }
          [Description("Name")]
    public string Name {get;set;} 
    [Description("Address")]
    public string? Address {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 
    [Description("Email")]
    public string? Email {get;set;} 
    [Description("Vat")]
    public string? VAT {get;set;} 
    [Description("Country")]
    public string? Country {get;set;} 


      public string CacheKey => SupplierCacheKey.GetAllCacheKey;
      public IEnumerable<string>? Tags => SupplierCacheKey.Tags;
}

public class AddEditSupplierCommandHandler : IRequestHandler<AddEditSupplierCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    public AddEditSupplierCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(AddEditSupplierCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            var item = await _context.Suppliers.FindAsync(request.Id, cancellationToken);
            if (item == null)
            {
                return await Result<int>.FailureAsync($"Supplier with id: [{request.Id}] not found.");
            }
            SupplierMapper.ApplyChangesFrom(request,item);
			// raise a update domain event
			item.AddDomainEvent(new SupplierUpdatedEvent(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = SupplierMapper.FromEditCommand(request);
            // raise a create domain event
			item.AddDomainEvent(new SupplierCreatedEvent(item));
            _context.Suppliers.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
       
    }
}

