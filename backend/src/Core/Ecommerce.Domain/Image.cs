using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Image : BaseDomainModel
    {
        public string Url { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string PublicCode { get; set; }
    }
}