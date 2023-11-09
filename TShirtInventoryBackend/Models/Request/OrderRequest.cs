namespace TshirtInventoryBackend.Models.Request
{
    public class OrderRequest
    {
        public int OrderNumber { get; set; }
        public string PaymentMethod { get; set; }
        public ICollection<TshirtOrderRequest> TshirtRequests { get; set; }
    }
}
