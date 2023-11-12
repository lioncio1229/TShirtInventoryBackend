using TshirtInventoryBackend.DTOs;

namespace TshirtInventoryBackend.Models.Reponse
{
    public class TshirtsResponse
    {
        public int Total { get; set; }
        public IEnumerable<Tshirt> tshirts { get; set; }
    }
}
