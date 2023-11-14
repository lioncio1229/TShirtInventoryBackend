using AutoMapper;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
        public bool IsActived { get; set; }
    }
}
