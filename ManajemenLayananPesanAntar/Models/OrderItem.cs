using System.ComponentModel.DataAnnotations.Schema;

namespace API_Manajemen_Layanan_Pesan_Antar.Models
{
    public class OrderItem : BaseModel
    {
        [ForeignKey("product_id")]
        public Product product { get; set; }
        public long product_id { get; set; }
        public long order_id { get; set; }
        public int order_qty { get; set; }
        public int unit_price { get; set; }
        public int subtotal { get; set; }
    }
}
