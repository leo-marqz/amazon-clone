
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class OrderItem : BaseDomainModel
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int CartItemId { get; set; }
        public string ImagenUrl { get; set; }
    }
}