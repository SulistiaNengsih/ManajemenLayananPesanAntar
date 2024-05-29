using API_Manajemen_Layanan_Pesan_Antar.Controllers;
using API_Manajemen_Layanan_Pesan_Antar.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API_Manajemen_Layanan_Pesan_Antar.Services
{
    public interface IUserService
    {
        public ResponseDataInfo<string> Register(RegisterRequest registerDto);
        public ResponseDataInfo<LoginResponse> Login(LoginRequest loginDto);
    }
}
