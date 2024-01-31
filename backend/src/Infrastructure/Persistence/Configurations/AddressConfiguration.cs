using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a=>a.DescribeAddress).HasMaxLength(120);
            builder.Property(a=>a.City).HasMaxLength(120);
            builder.Property(a=>a.Department).HasMaxLength(120);
            builder.Property(a=>a.Country).HasMaxLength(120);
            builder.Property(a=>a.PostalCode).HasMaxLength(25);
            builder.Property(a=>a.UserName).HasMaxLength(100);
        }
    }
}