using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasMany(c=>c.CartItems)
                   .WithOne(ci=>ci.Cart)
                   .HasForeignKey(ci=>ci.CartId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}