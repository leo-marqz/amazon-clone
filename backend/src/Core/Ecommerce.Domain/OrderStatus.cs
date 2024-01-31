using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Domain
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pendiente")]
        Pending,

        [EnumMember(Value = "Pago recibido")]
        Completed,

        [EnumMember(Value = "Orden enviada")]
        Shipped,

        [EnumMember(Value = "Orden entregada")]
        Delivered,

        [EnumMember(Value = "Error en proceso de la orden")]
        Error
    }
}