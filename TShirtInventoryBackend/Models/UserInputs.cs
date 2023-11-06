using TshirtInventoryBackend.Repositories.Common;

namespace TshirtInventoryBackend.Models
{
    public class UserRegistrationInputs
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }

    public class UserUpdateInputs
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }

    public class RoleUpdateInputs
    {
        public int RoleId { get; set; }
    }
}
