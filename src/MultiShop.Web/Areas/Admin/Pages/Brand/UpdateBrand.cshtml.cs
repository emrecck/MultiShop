using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.Web.Services.CatalogServices.BrandServices;

namespace MultiShop.Web.Areas.Admin.Pages.Brand
{
    public class UpdateBrandModel : PageModel
    {
        private readonly IBrandService _brandService;

        public UpdateBrandModel(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [BindProperty]
        public UpdateBrandDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Markalar";
            ViewData["v3"] = "Marka Listesi";
            ViewData["v0"] = "Marka İşlemleri";
            Input = await _brandService.GetByIdBrandAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _brandService.UpdateBrandAsync(Input);
            return RedirectToPage("/Brand/Index", new { area = "Admin" });
        }
    }
}
