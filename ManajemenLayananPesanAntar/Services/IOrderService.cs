using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using API_Manajemen_Layanan_Pesan_Antar.Utilities;
using Microsoft.AspNetCore.Mvc;
using static API_Manajemen_Layanan_Pesan_Antar.Controllers.OrderController;

namespace API_Manajemen_Layanan_Pesan_Antar.Services
{
    public interface IOrderService
    {
        public ResponseDataInfo<OrderDto> CreateOrder();
        public ResponseDataInfo<OrderDto> AddOrderItems(List<AddOrderItemsRequest> req, long id);
        public ResponseDataInfo<OrderDto> AddOrderDetail(AddOrderDetailRequest req, long id);
        public ResponseDataInfo<OrderDto> ProcessOrder(long id);
        public ResponseDataInfo<OrderDto> DeliverOrder(DeliverOrderRequest req, long id);
        public ResponseDataInfo<OrderDto> CompleteOrder(long id);
        public ResponseDataInfo<OrderDto> SuspendOrder(SuspendOrCancelOderRequest req, long id);
        public ResponseDataInfo<OrderDto> CancelOrder(SuspendOrCancelOderRequest req, long id);
        public ResponseDataInfo<List<OrderDto>> GetOrder();
        public ResponseDataInfo<OrderDto> GetOrderById(long id);
        public ResponseDataInfo<OrderDto> GetOrderByOrderNumber(string order_number);
        public ResponseDataInfo<OrderDeliveryDto> UpdateCourierLocation(long orderDeliveryId, string? latitude, string? longitude);
        public ResponseDataInfo<String> AddFcmToken(string token);
        public ResponseDataInfo<OrderDto> CreateOrderWithDetails(List<AddOrderItemsRequest> aoiReq, AddOrderDetailRequest aodReq);
    }
}
