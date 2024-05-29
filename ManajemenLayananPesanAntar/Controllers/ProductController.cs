using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using API_Manajemen_Layanan_Pesan_Antar.Services;
using API_Manajemen_Layanan_Pesan_Antar.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API_Manajemen_Layanan_Pesan_Antar.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _svc;

        public ProductController(IProductService svc)
        {
            _svc = svc;
        }

        [HttpPost]
        public ActionResult<ResponseDataInfo<ProductDto>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            var response = _svc.CreateProduct(createProductDto);
            return Ok(response);
        }

        [HttpGet("get_product")]
        public ResponseDataInfo<List<ProductDto>> GetProduct()
        {
            return _svc.GetProduct(); 
        }

        [HttpGet("get_product/{id}")]
        public ResponseDataInfo<ProductDto> GetProductById(long id)
        {
            return _svc.GetProductById(id);
        }

        public class CreateProductDto
        {
            public string product_name { get; set; }
            public string product_desc { get; set; }
            public int available_qty { get; set; }
            public int unit_price { get; set; }
            public string image_url { get; set; }
        }
    }
}
