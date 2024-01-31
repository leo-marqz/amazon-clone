
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain
{
    public class Order : BaseDomainModel
    {

        public Order(){}

        public Order(
            string buyerName, 
            string buyerUsernameOrEmail,
            OrderAddress orderAddress,
            decimal subTotal,
            decimal total,
            decimal taxes,
            decimal shippingPrice
        ){
            BuyerName = buyerName;
            BuyerUsername = buyerUsernameOrEmail;
            OrderAddress = orderAddress;
            SubTotal = subTotal;
            Total = total;
            Taxes = taxes;
            ShippingPrice = shippingPrice;
        }

        public string BuyerName { get; set; }
        public string BuyerUsername { get; set; }
        public OrderAddress OrderAddress { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Taxes { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ShippingPrice { get; set; }

        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public string StripeApiKey { get; set; }
    }
}