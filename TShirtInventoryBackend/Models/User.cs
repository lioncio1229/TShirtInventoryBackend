namespace TshirtInventoryBackend.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
        public bool IsActived { get; set; }
    }
}
