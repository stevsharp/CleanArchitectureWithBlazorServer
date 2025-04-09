// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Data;
using System.Reflection;
using CleanArchitecture.Blazor.Domain.Common.Entities;
using CleanArchitecture.Blazor.Domain.Identity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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

    public DbSet<PurchaseItem> PurchaseItems { get; set; }
    public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; } 
    public DbSet<SubCategory> SubCategories { get; set; }
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
    public DbSet<Step> Steps { get; set; }

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

    public async Task UpdateCategorySubCategoriesAsync(int categoryId, List<int> subCategoryIds, CancellationToken cancellationToken)
    {
        // Convert list to DataTable
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        foreach (var id in subCategoryIds.Distinct())
        {
            table.Rows.Add(id);
        }

        var categoryParam = new SqlParameter("@CategoryId", categoryId);
        var subCatParam = new SqlParameter("@SubCategoryIds", SqlDbType.Structured)
        {
            TypeName = "dbo.IntList",
            Value = table
        };

        await this.Database.ExecuteSqlRawAsync(
            "EXEC UpdateCategorySubCategories @CategoryId, @SubCategoryIds",
            new[] { categoryParam, subCatParam },
            cancellationToken);
    }

    public void AddOrUpdate<TEntity>(TEntity entity) where TEntity : class
    {
        if (this.Entry(entity).IsKeySet)
            this.Update(entity);
        else
            this.Add(entity);
    }
}