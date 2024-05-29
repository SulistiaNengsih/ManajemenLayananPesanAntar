using System.ComponentModel.DataAnnotations.Schema;

namespace API_Manajemen_Layanan_Pesan_Antar.Models
{
    public class Product : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string product_name { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string product_desc { get; set; }
        public int available_qty { get; set;}
        public int unit_price { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string image_url { get; set; }
    }
}
