using System;
using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o=>o.OrderAddress, x=>x.WithOwner()); // uno a uno
            builder.HasMany(o=>o.OrderItems)
                   .WithOne(oi=>oi.Order)
                   .HasForeignKey(oi=>oi.OrderId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(o=>o.Status)
                   .HasConversion(
                    o=>o.ToString(),
                    o=> (OrderStatus)Enum.Parse(typeof(OrderStatus), o));
        }
    }
}