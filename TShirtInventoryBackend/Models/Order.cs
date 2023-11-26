using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TshirtInventoryBackend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public Customer Customer { get; set; }
        public ICollection<TshirtOrder> TshirtOrders { get; set; }
    }
}
