using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CatalogServices.BrandServices;

namespace MultiShop.Web.Areas.Admin.Pages.Brand
{
    public class DeleteBrandModel : PageModel
    {
        private readonly IBrandService _brandService;

        public DeleteBrandModel(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _brandService.DeleteBrandAsync(id);
            return RedirectToPage("/Brand/Index", new { area = "Admin" });
        }
    }
}
