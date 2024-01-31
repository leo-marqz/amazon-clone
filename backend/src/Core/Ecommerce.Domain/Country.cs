
namespace Ecommerce.Domain
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISO2 { get; set; } = string.Empty;
        public string ISO3 { get; set; } = string.Empty;
    }
}