using API_Manajemen_Layanan_Pesan_Antar.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Manajemen_Layanan_Pesan_Antar.Models
{
    public class Order : BaseModel
    {
        [Column(TypeName = "varchar(10)")]
        public string? order_number { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? cust_name { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? cust_phone_number { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? suspend_remark { get; set; }
        public string? cancel_remark { get; set; }
        public OrderStatusEnum order_status { get; set; }
        public int? total_payment { get; set; }
        public int? cash_amount { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? delivered_by { get; set; }
        public DateTime? processed_at { get; set; }
        public DateTime? delivered_at { get; set; }
        public DateTime? received_at { get; set; }
        public DateTime? suspended_at { get; set; }
        public DateTime? canceled_at { get; set; }
        public List<OrderItem> order_items { get; set; }
        public OrderDelivery order_delivery { get; set; }
    }
}
