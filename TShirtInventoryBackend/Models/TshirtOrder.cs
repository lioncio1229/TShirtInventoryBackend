namespace TshirtInventoryBackend.Models
{
    public class TshirtOrder
    {
        public int TshirtId { get; set; }
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public Status Status { get; set; }
        public Tshirt Tshirt { get; set; }
        public Order Order { get; set; }
    }
}
