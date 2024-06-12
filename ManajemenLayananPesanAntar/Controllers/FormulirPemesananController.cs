using Microsoft.AspNetCore.Mvc;
using API_Manajemen_Layanan_Pesan_Antar.Services;

namespace API_Manajemen_Layanan_Pesan_Antar.Controllers
{
    public class FormulirPemesananController : Controller
    {
        public readonly IOrderService _svcOrder;
        public readonly IProductService _svcProduct;

        public FormulirPemesananController(IProductService svcProduct, IOrderService svcOrder)
        {
            _svcProduct = svcProduct;
            _svcOrder = svcOrder;
        }

        public IActionResult Index()
        {
            var listProduct = _svcProduct.GetProduct().data;
            return View(listProduct);
        }
    }
}
