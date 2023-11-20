using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.DTOs
{
    public class TshirtDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Design { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
        public string ProductImageUrl { get; set; }
        public Category Category { get; set; }
    }
}
