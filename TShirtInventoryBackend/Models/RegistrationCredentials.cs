using TshirtInventoryBackend.Repositories.Common;

namespace TshirtInventoryBackend.Models
{
    public class RegistrationCredentials
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
