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
//       This file defines the UpdateCrmProductCategoryCommand and its handler for updating 
//       an existing CrmProductCategory entity within the CleanArchitecture.Blazor application. 
//       It includes caching invalidation logic to maintain data consistency and 
//       raises a domain event upon successful update to notify other parts of the system.
//     
//     Documentation:
//       https://docs.cleanarchitectureblazor.com/features/crmproductcategory
// </auto-generated>
//------------------------------------------------------------------------------

// Usage:
// Use `UpdateCrmProductCategoryCommand` to update an existing crmproductcategory entity in the system. 
// The handler ensures that if the entity is found, the changes are applied and 
// the necessary domain event (`CrmProductCategoryUpdatedEvent`) is raised. Caching is also 
// invalidated to keep the crmproductcategory list consistent.

using CleanArchitecture.Blazor.Application.Features.CrmProductCategories.Caching;
using CleanArchitecture.Blazor.Application.Features.CrmProductCategories.Mappers;

namespace CleanArchitecture.Blazor.Application.Features.CrmProductCategories.Commands.Update;

public class UpdateCrmProductCategoryCommand: ICacheInvalidatorRequest<Result<int>>
{
      [Description("Id")]
      public int Id { get; set; }
            [Description("Category name")]
    public string? CategoryName {get;set;} 
    [Description("Crm product id")]
    public int CrmProductId {get;set;} 

      public string CacheKey => CrmProductCategoryCacheKey.GetAllCacheKey;
      public IEnumerable<string>? Tags => CrmProductCategoryCacheKey.Tags;

}

public class UpdateCrmProductCategoryCommandHandler : IRequestHandler<UpdateCrmProductCategoryCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    public UpdateCrmProductCategoryCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result<int>> Handle(UpdateCrmProductCategoryCommand request, CancellationToken cancellationToken)
    {

       var item = await _context.CrmProductCategories.FindAsync(request.Id, cancellationToken);
       if (item == null)
       {
           return await Result<int>.FailureAsync($"CrmProductCategory with id: [{request.Id}] not found.");
       }
       CrmProductCategoryMapper.ApplyChangesFrom(request, item);
	    // raise a update domain event
	   item.AddDomainEvent(new CrmProductCategoryUpdatedEvent(item));
       await _context.SaveChangesAsync(cancellationToken);
       return await Result<int>.SuccessAsync(item.Id);
    }
}

