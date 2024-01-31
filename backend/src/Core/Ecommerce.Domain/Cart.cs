
using System;
using System.Collections.Generic;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    //ShoppingCart
    public class Cart : BaseDomainModel
    {
        //ShoppingCartMasterId
        public Guid CartMasterId { get; set; }
        
        //ShoppingCartItems
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}