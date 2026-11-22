using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Web.Services.CatalogServices.ProductServices;

namespace MultiShop.Web.Areas.Admin.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<ResultProductDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Ürünler";
            ViewData["v3"] = "Ürün Listesi";
            ViewData["v0"] = "Ürün İşlemleri";
            Items = await _productService.GetAllProductAsync();
        }
    }
}
