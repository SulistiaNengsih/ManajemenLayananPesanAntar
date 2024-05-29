using System.ComponentModel.DataAnnotations.Schema;

namespace API_Manajemen_Layanan_Pesan_Antar.DTOs
{
    public class OrderDeliveryDto : BaseDto
    {
        public long order_id { get; set; }
        public string? delivery_address { get; set; }
        public string? delivery_remark { get; set; }
        public string? location_name { get; set; }
        public double? delivery_latitude { get; set; }
        public double? delivery_longitude { get; set; }
        public double? courier_latitude { get; set; }
        public double? courier_longitude { get; set; }
    }
}
