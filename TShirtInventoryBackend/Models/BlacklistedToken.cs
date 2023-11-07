using System.ComponentModel.DataAnnotations;

namespace TshirtInventoryBackend.Models
{
    public class BlacklistedToken
    {
        public int Id { get; set; }
        public string Token { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateBlacklisted { get; set; }
    }
}
