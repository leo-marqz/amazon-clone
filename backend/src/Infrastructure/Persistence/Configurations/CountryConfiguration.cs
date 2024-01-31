

using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c=>c.Name).HasMaxLength(50);
            builder.Property(c=>c.ISO2).HasMaxLength(2);
            builder.Property(c=>c.ISO3).HasMaxLength(3);
        }
    }
}