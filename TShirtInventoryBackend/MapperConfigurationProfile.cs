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
            CreateMap<User, UserDTO>();
            CreateMap<Tshirt, TshirtDTO>();

            CreateMap<Order, OrderDTO>();

            CreateMap<Customer, CustomerDTO>();

            CreateMap<TshirtOrder, TshirtOrderDTO>();
        }
    }
}
