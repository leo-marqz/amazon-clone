using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Address : BaseDomainModel
    {
        public string DescribeAddress { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string PostalCode { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
    }
}