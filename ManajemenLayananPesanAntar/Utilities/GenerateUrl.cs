using API_Manajemen_Layanan_Pesan_Antar.Enums;

namespace API_Manajemen_Layanan_Pesan_Antar.Utilities
{
    public class GenerateUrl
    {
        public static string GenerateSendWhatsAppUrl(string phone_number, string message)
        {
            var encodedMessage = Uri.EscapeDataString(message);
            var link = $"https://api.whatsapp.com/send?phone={phone_number}&text={encodedMessage}";
            return link;
        }

        public static string GenerateTrackingUrl(OrderStatusEnum orderStatus)
        {
            var trackingUrl = "";
            if (orderStatus != OrderStatusEnum.Temporer)
            {
                trackingUrl = $"https://";
            }
            return trackingUrl;
        }
    }
}
