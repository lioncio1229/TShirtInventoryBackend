namespace TshirtInventoryBackend.Models.Request
{
    public class TshirtRequest
    {
        public string Name { get; set; }
        public string Design { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
    }
}
