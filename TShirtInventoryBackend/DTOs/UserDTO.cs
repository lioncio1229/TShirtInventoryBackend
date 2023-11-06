using AutoMapper;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
