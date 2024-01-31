
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    //ShoppingCartItem
    public class CartItem : BaseDomainModel
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int ProductStock { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }

        //shoppingCartMasterId
        public Guid CartMasterId { get; set; }

        //ShoppingCartId
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}