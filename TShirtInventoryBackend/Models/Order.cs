using TshirtInventoryBackend.DTOs;

namespace TshirtInventoryBackend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string PaymentMethod { get; set; }
        public Status Status { get; set; }
        public Customer Customer { get; set; }
        public ICollection<TshirtOrder> TshirtOrders { get; set; }
    }
}
