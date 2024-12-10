﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-06
//     Last Modified: 2024-12-06
//     Description: 
//       Configures the properties and behaviors for the `Supplier` entity in the 
//       database. Specifies property constraints such as maximum length and required fields.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
            builder.HasIndex(x => x.Name); 
    builder.Property(x => x.Name).HasMaxLength(50).IsRequired(); 
    builder.Property(x => x.Address).HasMaxLength(255); 
    builder.Property(x => x.Phone).HasMaxLength(255); 
    builder.Property(x => x.Email).HasMaxLength(255); 
    builder.Property(x => x.VAT).HasMaxLength(255); 
    builder.Property(x => x.Country).HasMaxLength(255); 

        builder.Ignore(e => e.DomainEvents);
    }
}
