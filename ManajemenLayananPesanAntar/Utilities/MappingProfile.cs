using AutoMapper;
using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using API_Manajemen_Layanan_Pesan_Antar.Models;
using API_Manajemen_Layanan_Pesan_Antar.Repositories;
using System.Globalization;
using Microsoft.CodeAnalysis.CSharp;

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
                .ForMember(dest => dest.order_status, opt => opt.MapFrom(src => src.order_status.ToString().Replace("_", " ")))
                .ForMember(dest => dest.formatted_cash_amount, opt => opt.MapFrom(src => OrderRepository.ConvertToRupiah(src.cash_amount)))
                .ForMember(dest => dest.formatted_total_payment, opt => opt.MapFrom(src => OrderRepository.ConvertToRupiah(src.total_payment)));
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.order_status, opt => opt.MapFrom(src => src.order_status.ToString().Replace(" ", "_")));

            // Order Item
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.formatted_subtotal, opt => opt.MapFrom(src => OrderRepository.ConvertToRupiah(src.subtotal)))
                .ForMember(dest => dest.formatted_unit_price, opt => opt.MapFrom(src => OrderRepository.ConvertToRupiah(src.unit_price)));
            CreateMap<OrderItemDto, OrderItem>();

            // Order Delivery
            CreateMap<OrderDelivery, OrderDeliveryDto>()
                .ForMember(dest => dest.deliverylatlng, opt => opt.MapFrom(src => $"{src.delivery_latitude},{src.delivery_longitude}"));
            CreateMap<OrderDeliveryDto, OrderDelivery>();
        }
    }
}
