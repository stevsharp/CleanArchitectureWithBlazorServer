// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Text.Json;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Serialization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;
#nullable disable
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x=>x.Name).HasMaxLength(80).IsRequired();
        builder.Ignore(e => e.DomainEvents);
        builder.Property(e => e.Pictures)
            .HasConversion(
                v => JsonSerializer.Serialize(v, DefaultJsonSerializerOptions.Options),
                v => JsonSerializer.Deserialize<List<ProductImage>>(v, DefaultJsonSerializerOptions.Options),
                new ValueComparer<List<ProductImage>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));


        builder.OwnsMany(p => p.UnitOptions, unitBuilder =>
        {
            unitBuilder.ToTable("ProductUnitOptions");

            // Explicit foreign key (shadow property) — must match exactly!
            unitBuilder.WithOwner().HasForeignKey("ProductId");

            // Shadow key
            unitBuilder.Property<int>("Id");
            unitBuilder.HasKey("Id");
            unitBuilder.Property(u => u.UnitId).IsRequired();
            unitBuilder.Property(u => u.Name).HasMaxLength(50).IsRequired();
            unitBuilder.Property(u => u.Value).HasMaxLength(50).IsRequired();
            unitBuilder.Property(u => u.Text).HasMaxLength(50).IsRequired();
            unitBuilder.Property(u => u.Description).HasMaxLength(255);
        });

        builder.OwnsMany(p => p.ColorOptions, colorBuilder =>
        {
            colorBuilder.ToTable("ProductColorOptions");

            colorBuilder.WithOwner().HasForeignKey("ProductId");

            colorBuilder.Property<int>("Id");
            colorBuilder.HasKey("Id");
            colorBuilder.Property(u => u.UnitId).IsRequired();
            colorBuilder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            colorBuilder.Property(c => c.Value).HasMaxLength(50).IsRequired();
            colorBuilder.Property(c => c.Text).HasMaxLength(50).IsRequired();
            colorBuilder.Property(c => c.Description).HasMaxLength(255);
        });

        //builder.OwnsMany(o => o.SubProducts, subBuilder =>
        //{
        //    subBuilder.ToTable("SubProduct"); // Map to a separate table
        //    subBuilder.WithOwner().HasForeignKey("ProdId"); // Foreign key to Product
        //    subBuilder.Property(sp => sp.Unit).HasMaxLength(50); // Example property configuration
        //    subBuilder.Property(sp => sp.Color).HasMaxLength(50); // Example property configuration

        //});

    }
}