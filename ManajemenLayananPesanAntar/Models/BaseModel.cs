namespace API_Manajemen_Layanan_Pesan_Antar.Models
{
    public class BaseModel
    {
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public void SetCreated()
        {
            created_at = DateTime.UtcNow.AddHours(7);
            updated_at = DateTime.UtcNow.AddHours(7);
        }
        public void SetUpdated()
        {
            updated_at = DateTime.UtcNow.AddHours(7);
        }
    }
}
