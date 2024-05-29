using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using API_Manajemen_Layanan_Pesan_Antar.Enums;
using API_Manajemen_Layanan_Pesan_Antar.Models;
using API_Manajemen_Layanan_Pesan_Antar.Repositories;
using API_Manajemen_Layanan_Pesan_Antar.Services;
using API_Manajemen_Layanan_Pesan_Antar.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;

namespace API_Manajemen_Layanan_Pesan_Antar.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _svc;

        public UserController(IUserService svc)
        {
            _svc = svc;
        }

        [HttpPost("register")]
        public ActionResult<ResponseDataInfo<string>> Register([FromBody] RegisterRequest registerDto)
        {
            var response = _svc.Register(registerDto);
            return Ok(response);
        }

        [HttpPost("login")]
        public ActionResult<ResponseDataInfo<LoginResponse>> Login([FromBody] LoginRequest loginDto)
        {
            var response = _svc.Login(loginDto);
            return Ok(response);
        }
    }

    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class LoginResponse
    {
        public UserDto logged_in_user { get; set; }
        public string token { get; set; }
    }

    public class RegisterRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public RoleEnum role { get; set; }
    }
}

