using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.DTOs
{
    public class TshirtOrderDTO
    {
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public TshirtDTO Tshirt { get; set; }
    }
}
