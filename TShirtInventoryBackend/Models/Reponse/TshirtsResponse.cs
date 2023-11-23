using TshirtInventoryBackend.DTOs;

namespace TshirtInventoryBackend.Models.Reponse
{
    public class TshirtsResponse
    {
        public int TotalQuery { get; set; }
        public int Total { get; set; }
        public IEnumerable<TshirtDTO> tshirts { get; set; }
    }
}
