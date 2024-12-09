﻿

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offer");

       builder.Property(x => x.Status).HasMaxLength(255);

        builder.OwnsMany(o => o.OfferLines, x =>
        {
            x.ToTable("OfferLine"); // Map to separate table

            x.WithOwner().HasForeignKey("OfferId"); // Foreign key

        });

        builder.Ignore(e => e.DomainEvents);
    }
}


