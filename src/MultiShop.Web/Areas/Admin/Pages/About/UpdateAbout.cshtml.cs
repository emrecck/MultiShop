using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.Web.Services.CatalogServices.AboutServices;

namespace MultiShop.Web.Areas.Admin.Pages.About
{
    public class UpdateAboutModel : PageModel
    {
        private readonly IAboutService _aboutService;

        public UpdateAboutModel(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [BindProperty]
        public UpdateAboutDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Hakkımda";
            ViewData["v3"] = "Hakkımda Listesi";
            ViewData["v0"] = "Hakkımda İşlemleri";
            Input = await _aboutService.GetByIdAboutAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _aboutService.UpdateAboutAsync(Input);
            return RedirectToPage("/About/Index", new { area = "Admin" });
        }
    }
}
