﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Logger> Loggers { get; set; }
    DbSet<AuditTrail> AuditTrails { get; set; }
    DbSet<Document> Documents { get; set; }
    DbSet<PicklistSet> PicklistSets { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Supplier> Suppliers { get; set; }
    DbSet<Tenant> Tenants { get; set; }
    DbSet<Contact> Contacts { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<PurchaseItem> PurchaseItems { get; set; }
    DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }   
    DbSet<Offer> Offers { get; set; }
    DbSet<SupplyItem> SupplyItems { get; set; }
    DbSet<SubProduct> SubProducts { get; set; }
    DbSet<Invoice> Invoices { get; set; }
    DbSet<SubCategory> SubCategories { get; set; }
    DbSet<Step> Steps { get; set; }
    ChangeTracker ChangeTracker { get; }
    DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task UpdateCategorySubCategoriesAsync(int categoryId, List<int> subCategoryIds, CancellationToken cancellationToken);

    void AddOrUpdate<TEntity>(TEntity entity) where TEntity : class;
}