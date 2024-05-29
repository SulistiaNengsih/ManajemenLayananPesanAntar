using API_Manajemen_Layanan_Pesan_Antar.Controllers;
using API_Manajemen_Layanan_Pesan_Antar.Services;
using API_Manajemen_Layanan_Pesan_Antar.Utilities;
using API_Manajemen_Layanan_Pesan_Antar.Models;
using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;

namespace API_Manajemen_Layanan_Pesan_Antar.Repositories
{
    public class UserRepository : IUserService
    {
        private readonly IConfiguration _configuration;
        public readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(IConfiguration configuration, ApplicationDbContext dbContext, IMapper mapper)
        {
            _configuration = configuration;
            _dbContext = dbContext;            _mapper = mapper;

        }

        public ResponseDataInfo<string> Register(RegisterRequest registerDto)
        {
            ResponseDataInfo<string> response = new ResponseDataInfo<string>()
            {
                data = null,
                info = ""
            };

            if (registerDto.username.IsNullOrEmpty()
                || registerDto.username.IsNullOrEmpty()
                || registerDto.name.IsNullOrEmpty()
                || registerDto.role == null)
            {
                response.info = "Data registrasi tidak boleh kosong.";
                return response;
            }

            var existingUser = _dbContext.Set<User>().Where(x => x.username == registerDto.username).FirstOrDefault();

            if (existingUser != null)
            {
                response.info = "User dengan username tersebut sudah terdaftar.";
                return response;
            }

            if (registerDto.password.Length < 6)
            {
                response.info = "Password tidak boleh kurang dari 6 karakter.";
                return response;
            }

            User newUser = new User()
            {
                username = registerDto.username,
                password = HashPassword(registerDto.password),
                name = registerDto.name,
                role = registerDto.role,
                isActive = true
            };

            newUser.SetCreated();
            _dbContext.Add<User>(newUser);
            _dbContext.SaveChanges();
            response.info = "Registrasi berhasil.";

            return response;
        }

        public ResponseDataInfo<LoginResponse> Login(LoginRequest loginDto)
        {
            ResponseDataInfo<LoginResponse> response = new ResponseDataInfo<LoginResponse>()
            {
                data = null,
                info = ""
            };

            if (loginDto.username.IsNullOrEmpty() || loginDto.password.IsNullOrEmpty())
            {
                response.info = "Data login tidak boleh kosong,";
                return response;
            }

            var user = _dbContext.Set<User>().Where(x => x.username == loginDto.username).FirstOrDefault();

            if (user == null || !VerifyPassword(loginDto.password, user.password))
            {
                response.info = "Username atau password salah.";
                return response;
            }

            var token = GenerateJwtToken(user.username);
            var userDto = _mapper.Map<UserDto>(user);
            response.data = new LoginResponse()
            {
                logged_in_user = userDto,
                token = token
            };
            response.info = "Login berhasil.";
            return response;
        }

        private string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
