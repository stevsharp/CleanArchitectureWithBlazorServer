﻿// Licensed to the .NET Foundation under one or more agreements.
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
    public DbSet<Logger> Loggers { get; set; }
    public DbSet<AuditTrail> AuditTrails { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<PicklistSet> PicklistSets { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<SupplyItem> SupplyItems { get; set; }
    public DbSet<SubProduct> SubProducts { get; set; }

    //public DbSet<OfferLine> OfferLines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyGlobalFilters<ISoftDelete>(s => s.Deleted == null);
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