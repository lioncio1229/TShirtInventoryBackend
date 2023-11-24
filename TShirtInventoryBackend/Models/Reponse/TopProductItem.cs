using TshirtInventoryBackend.DTOs;

namespace TshirtInventoryBackend.Models.Reponse
{
    public class TopProductItem
    {
        public TshirtDTO Tshirt { get; set; }
        public int TotalSaleQuantity { get; set; }
    }
}
