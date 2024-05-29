using API_Manajemen_Layanan_Pesan_Antar.Enums;
using API_Manajemen_Layanan_Pesan_Antar.Models;

namespace API_Manajemen_Layanan_Pesan_Antar.DTOs
{
    public class OrderDto : BaseDto
    {
        public string? order_number { get; set; }
        public string? cust_name { get; set; }
        public string? cust_phone_number { get; set; }
        public string order_status { get; set; }
        public int? total_payment { get; set; }
        public int? cash_amount { get; set; }
        public string? delivered_by { get; set; }
        public string? suspend_remark { get; set; }
        public string? cancel_remark { get; set; }
        public string? tracking_url { get; set; }
        public string? send_whatsapp_url { get; set; }
        public DateTime? processed_at { get; set; }
        public DateTime? delivered_at { get; set; }
        public DateTime? received_at { get; set; }
        public DateTime? suspended_at { get; set; }
        public DateTime? canceled_at { get; set; }
        public List<OrderItem> order_items { get; set; }
        public OrderDelivery order_delivery { get; set; }
    }
}       