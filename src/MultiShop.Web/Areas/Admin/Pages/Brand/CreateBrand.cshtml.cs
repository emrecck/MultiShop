using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.Web.Services.CatalogServices.BrandServices;

namespace MultiShop.Web.Areas.Admin.Pages.Brand
{
    public class CreateBrandModel : PageModel
    {
        private readonly IBrandService _brandService;

        public CreateBrandModel(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [BindProperty]
        public CreateBrandDto Input { get; set; } = new();

        public void OnGet()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Markalar";
            ViewData["v3"] = "Marka Listesi";
            ViewData["v0"] = "Marka İşlemleri";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _brandService.CreateBrandAsync(Input);
            return RedirectToPage("/Brand/Index", new { area = "Admin" });
        }
    }
}
