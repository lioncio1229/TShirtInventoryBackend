namespace TshirtInventoryBackend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string Status { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Tshirt> Tshirts { get; set; }
    }
}
