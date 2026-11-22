using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.Web.Services.CatalogServices.BrandServices;

namespace MultiShop.Web.Areas.Admin.Pages.Brand
{
    public class IndexModel : PageModel
    {
        private readonly IBrandService _brandService;

        public IndexModel(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public List<ResultBrandDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Markalar";
            ViewData["v3"] = "Marka Listesi";
            ViewData["v0"] = "Marka İşlemleri";
            Items = await _brandService.GetAllBrandAsync();
        }
    }
}
