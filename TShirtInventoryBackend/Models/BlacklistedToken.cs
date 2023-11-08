using System.ComponentModel.DataAnnotations;

namespace TshirtInventoryBackend.Models
{
    public class BlacklistedToken
    {
        [Key]
        public string Jti { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateBlacklisted { get; set; }
    }
}
