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

        builder.Property(x => x.CompanyType)
            .HasMaxLength(100);

        builder.Property(x => x.IBAN)
            .HasMaxLength(34);

        builder.Property(x => x.SWIFT)
            .HasMaxLength(11);

        builder.Property(x => x.ContactPerson)
            .HasMaxLength(100);

        builder.Property(x => x.Notes);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);



        builder.Ignore(e => e.DomainEvents);
    }
}

