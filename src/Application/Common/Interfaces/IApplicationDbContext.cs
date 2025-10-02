// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Domain.Identity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces;

public interface IApplicationDbContext : IAsyncDisposable
{
    DbSet<SystemLog> SystemLogs { get; set; }
    DbSet<AuditTrail> AuditTrails { get; set; }
    DbSet<Document> Documents { get; set; }
    DbSet<PicklistSet> PicklistSets { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Tenant> Tenants { get; set; }
    DbSet<Contact> Contacts { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<ServiceCategory> ServiceCategories { get; set; }
    DbSet<Service> Services { get; set; }
    DbSet<ServiceVariant> ServiceVariants { get; set; }
    DbSet<PricingModel> PricingModels { get; set; }
    DbSet<Vendor> Vendors { get; set; }
    DbSet<Venue> Venues { get; set; }
    DbSet<EquipmentItem> EquipmentItems { get; set; }
    DbSet<Quote> Quotes { get; set; }
    DbSet<QuoteVersion> QuoteVersions { get; set; }
    DbSet<QuoteLine> QuoteLines { get; set; }
    DbSet<QuoteAttachment> QuoteAttachments { get; set; }
    DbSet<QuoteApproval> QuoteApprovals { get; set; }
    DbSet<Project> Projects { get; set; }
    DbSet<Assignment> Assignments { get; set; }
    DbSet<ProjectTask> ProjectTasks { get; set; }
    DbSet<CostItem> CostItems { get; set; }
    DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    DbSet<LoginAudit> LoginAudits { get; set; }
    DbSet<UserLoginRiskSummary> UserLoginRiskSummaries { get; set; }

    DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    ChangeTracker ChangeTracker { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
