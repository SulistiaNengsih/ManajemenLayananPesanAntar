using AutoMapper;
using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using API_Manajemen_Layanan_Pesan_Antar.Models;

namespace API_Manajemen_Layanan_Pesan_Antar.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            // Product
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            // Order
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.order_status, opt => opt.MapFrom(src => src.order_status.ToString().Replace("_", " ")));
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.order_status, opt => opt.MapFrom(src => src.order_status.ToString().Replace(" ", "_")));
        }
    }
}
