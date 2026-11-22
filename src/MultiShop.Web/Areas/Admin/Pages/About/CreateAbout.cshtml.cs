using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.Web.Services.CatalogServices.AboutServices;

namespace MultiShop.Web.Areas.Admin.Pages.About
{
    public class CreateAboutModel : PageModel
    {
        private readonly IAboutService _aboutService;

        public CreateAboutModel(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [BindProperty]
        public CreateAboutDto Input { get; set; } = new();

        public void OnGet()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Hakkımda";
            ViewData["v3"] = "Hakkımda Listesi";
            ViewData["v0"] = "Hakkımda İşlemleri";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _aboutService.CreateAboutAsync(Input);
            return RedirectToPage("/About/Index", new { area = "Admin" });
        }
    }
}
