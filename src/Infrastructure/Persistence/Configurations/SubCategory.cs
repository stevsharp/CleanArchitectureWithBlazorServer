using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

public class SubCategoryConfiguration : IEntityTypeConfiguration<Domain.Entities.SubCategory>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.SubCategory> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .HasMany(e => e.Categories)
            .WithMany(e => e.SubCategories);
    }
}
