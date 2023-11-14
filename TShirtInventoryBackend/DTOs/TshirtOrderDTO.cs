using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.DTOs
{
    public class TshirtOrderDTO
    {
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public Status Status { get; set; }
        public Tshirt Tshirt { get; set; }
    }
}
