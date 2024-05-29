using API_Manajemen_Layanan_Pesan_Antar.DTOs;
using API_Manajemen_Layanan_Pesan_Antar.Models;
using API_Manajemen_Layanan_Pesan_Antar.Services;
using API_Manajemen_Layanan_Pesan_Antar.Utilities;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using static API_Manajemen_Layanan_Pesan_Antar.Controllers.ProductController;

namespace API_Manajemen_Layanan_Pesan_Antar.Repositories
{
    public class ProductRepository : IProductService
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ResponseDataInfo<ProductDto> CreateProduct(CreateProductDto createProductDto)
        {
            if (createProductDto.product_name.IsNullOrEmpty()
                || createProductDto.product_desc.IsNullOrEmpty()
                || createProductDto.available_qty == null
                || createProductDto.unit_price == 0)
            {
                return new ResponseDataInfo<ProductDto>
                {
                    data = null,
                    info = "Nama produk, deskripsi produk, atau stok tersedia tidak boleh kosong."
                };
            }

            Product newProduct = new Product()
            {
                product_name = createProductDto.product_name,
                product_desc = createProductDto.product_desc,
                available_qty = createProductDto.available_qty,
                unit_price = createProductDto.unit_price,
                image_url = createProductDto.image_url.IsNullOrEmpty() ? "" : createProductDto.image_url
            };

            newProduct.SetCreated();
            _dbContext.Add<Product>(newProduct);
            _dbContext.SaveChanges();

            var newProductDto = _mapper.Map<ProductDto>(newProduct);

            return new ResponseDataInfo<ProductDto>
            {
                data = newProductDto,
                info = "Produk berhasil ditambahkan."
            };
        }
        public ResponseDataInfo<List<ProductDto>> GetProduct()
        {
            var products = _dbContext.Set<Product>().ToList();

            if (products == null)
            {
                return new ResponseDataInfo<List<ProductDto>>
                {
                    data = null,
                    info = "Produk kosong."
                };
            }

            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return new ResponseDataInfo<List<ProductDto>>
            {
                data = productsDto,
                info = "Ok"
            };
        }
        public ResponseDataInfo<ProductDto> GetProductById(long id)
        {
            var product = _dbContext.Set<Product>().Where(x => x.id == id).FirstOrDefault();

            if (product == null)
            {
                return new ResponseDataInfo<ProductDto>
                {
                    data = null,
                    info = "Produk tidak ditemukan."
                };
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return new ResponseDataInfo<ProductDto>
            {
                data = productDto,
                info = "Ok"
            };
        }
    }
}
