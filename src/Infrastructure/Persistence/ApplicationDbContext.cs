// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Reflection;
using CleanArchitecture.Blazor.Domain.Common.Entities;
using CleanArchitecture.Blazor.Domain.Identity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence;

#nullable disable
public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser, ApplicationRole, string,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>, IApplicationDbContext, IDataProtectionKeyContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<SystemLog> SystemLogs { get; set; }
    public DbSet<AuditTrail> AuditTrails { get; set; }
    public DbSet<Document> Documents { get; set; }

    public DbSet<PicklistSet> PicklistSets { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<LoginAudit> LoginAudits { get; set; }
    public DbSet<UserLoginRiskSummary> UserLoginRiskSummaries { get; set; }
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    public DbSet<Venue> Venues { get; set; }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceVariant> ServiceVariants { get; set; }
    public DbSet<PricingModel> PricingModels { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<EquipmentItem> EquipmentItems { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<QuoteVersion> QuoteVersions { get; set; }
    public DbSet<QuoteLine> QuoteLines { get; set; }
    public DbSet<QuoteAttachment> QuoteAttachments { get; set; }
    public DbSet<QuoteApproval> QuoteApprovals { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<CostItem> CostItems { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyGlobalFilters<ISoftDelete>(s => s.DeletedAt == null);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<string>().HaveMaxLength(450);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
}