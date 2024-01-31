
using System.Collections.Generic;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Category : BaseDomainModel
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products {get; set;}
    }
}