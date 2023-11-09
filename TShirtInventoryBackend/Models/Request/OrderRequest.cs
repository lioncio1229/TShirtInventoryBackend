namespace TshirtInventoryBackend.Models.Request
{
    public class OrderRequest
    {
        public string PaymentMethod { get; set; }
        public ICollection<TshirtOrderRequest> TshirtRequests { get; set; }
    }
}
