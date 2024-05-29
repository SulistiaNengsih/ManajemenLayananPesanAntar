using API_Manajemen_Layanan_Pesan_Antar.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Manajemen_Layanan_Pesan_Antar.Models
{
    public class User : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public RoleEnum role { get; set; }
        public bool isActive { get; set; }
    }
}
 