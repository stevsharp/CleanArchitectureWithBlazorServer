// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class AllergyConfiguration : IEntityTypeConfiguration<Allergy>
{
    public void Configure(EntityTypeBuilder<Allergy> builder)
    {
            builder.Property(x => x.AllergyType).HasMaxLength(255); 
    builder.Property(x => x.Comments).HasMaxLength(255); 

        builder.Ignore(e => e.DomainEvents);
    }
}


