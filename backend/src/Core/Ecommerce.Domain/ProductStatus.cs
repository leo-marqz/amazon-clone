using System.Runtime.Serialization;
namespace Ecommerce.Domain
{
    public enum ProductStatus
    {
        [EnumMember(Value = "Product Activo")]
        Active = 1,
        
        [EnumMember(Value = "Product Inactivo")]
        Inactive = 0
    }
}