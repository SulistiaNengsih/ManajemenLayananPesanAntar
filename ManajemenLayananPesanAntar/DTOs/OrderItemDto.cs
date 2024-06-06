using API_Manajemen_Layanan_Pesan_Antar.Models;

namespace API_Manajemen_Layanan_Pesan_Antar.DTOs
{
    public class OrderItemDto : BaseDto
    {
        public int order_qty { get; set; }
        public int unit_price { get; set; }
        public string formatted_unit_price { get; set; }
        public int subtotal { get; set; }
        public string formatted_subtotal { get; set; }
        public Product product { get; set; }
    }
}
