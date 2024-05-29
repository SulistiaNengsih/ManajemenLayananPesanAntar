using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using API_Manajemen_Layanan_Pesan_Antar.Utilities;
using Microsoft.AspNetCore.Mvc;
using static API_Manajemen_Layanan_Pesan_Antar.Controllers.ProductController;

namespace API_Manajemen_Layanan_Pesan_Antar.Services
{
    public interface IProductService
    {
        public ResponseDataInfo<ProductDto> CreateProduct(CreateProductDto createProductDto);
        public ResponseDataInfo<List<ProductDto>> GetProduct();
        public ResponseDataInfo<ProductDto> GetProductById(long id);
    }
}
