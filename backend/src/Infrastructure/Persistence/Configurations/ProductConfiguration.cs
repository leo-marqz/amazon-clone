
using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)").IsRequired(); 
            builder.HasMany(p=>p.Reviews) //producto tiene muchas reviews
                   .WithOne(r=>r.Product) //una review tiene un producto
                   .HasForeignKey(r=>r.ProductId) //clave foranea en review
                   .IsRequired() //esta relacion es obligatoria
                   .OnDelete(DeleteBehavior.Cascade); //si elimino el producto, todas las revies se eliminaran
            
            builder.HasMany(p=>p.Images)
                   .WithOne(i=>i.Product)
                   .HasForeignKey(i=>i.ProductId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}