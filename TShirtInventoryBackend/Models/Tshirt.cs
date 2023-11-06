namespace TShirtInventoryBackend.Models
{
    public class Tshirt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Design { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
        public Category Category { get; set; }
    }
}
