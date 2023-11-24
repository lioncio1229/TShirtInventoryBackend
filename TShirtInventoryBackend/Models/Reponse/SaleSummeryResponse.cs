namespace TshirtInventoryBackend.Models.Reponse
{
    public class SaleSummeryResponse
    {
        public int AllCount { get; set; }
        public int QueueCount { get; set; }
        public int ProcessedCount { get; set; }
        public int ShippedCount { get; set; }
        public int DeliveredCount { get; set; }
    }
}
