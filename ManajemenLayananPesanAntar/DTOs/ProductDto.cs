using System.ComponentModel.DataAnnotations.Schema;

namespace API_Manajemen_Layanan_Pesan_Antar.DTOs
{
    public class ProductDto : BaseDto
    {
        public string product_name { get; set; }
        public string product_desc { get; set; }
        public int available_qty { get; set; }
        public int unit_price { get; set; }
        public string image_url { get; set; }
    }
}
