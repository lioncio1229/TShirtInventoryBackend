using AutoMapper;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Request;

namespace TshirtInventoryBackend
{
    public class MapperConfigurationProfile : Profile
    {
        public MapperConfigurationProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name));

            CreateMap<Customer, CustomerDTO>();

            CreateMap<TshirtOrder, TshirtOrderDTO>();
        }
    }
}
