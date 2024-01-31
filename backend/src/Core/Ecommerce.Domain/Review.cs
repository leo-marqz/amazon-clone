using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Review : BaseDomainModel
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}