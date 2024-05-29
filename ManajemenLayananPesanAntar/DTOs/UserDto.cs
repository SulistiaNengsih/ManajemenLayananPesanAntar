using API_Manajemen_Layanan_Pesan_Antar.Enums;

namespace API_Manajemen_Layanan_Pesan_Antar.DTOs
{
    public class UserDto : BaseDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public RoleEnum role { get; set; }
        public bool isActive { get; set; }
    }
}
