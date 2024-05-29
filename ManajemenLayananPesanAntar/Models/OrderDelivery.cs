using System.ComponentModel.DataAnnotations.Schema;

namespace API_Manajemen_Layanan_Pesan_Antar.Models
{
    public class OrderDelivery : BaseModel
    {
        public long order_id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? delivery_address { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? delivery_remark { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string? location_name { get; set; }
        [Column(TypeName = "decimal(8,6)")]
        public double delivery_latitude { get; set; }
        [Column(TypeName = "decimal(9,6)")]
        public double delivery_longitude { get; set; }
        [Column(TypeName = "decimal(8,6)")]
        public double? courier_latitude { get; set; }
        [Column(TypeName = "decimal(9,6)")]
        public double? courier_longitude { get; set; }
    }
}
