using System.ComponentModel.DataAnnotations.Schema;

namespace API_Manajemen_Layanan_Pesan_Antar.DTOs
{
    public class OrderDeliveryDto : BaseDto
    {
        public long order_id { get; set; }
        public string? delivery_address { get; set; }
        public string? delivery_remark { get; set; }
        public string? location_name { get; set; }
        public string? delivery_latitude { get; set; }
        public string? delivery_longitude { get; set; }
        public string? courier_latitude { get; set; }
        public string? courier_longitude { get; set; }
        public string? deliverylatlng { get; set; }
    }
}
