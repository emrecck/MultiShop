using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.Web.Services.CatalogServices.AboutServices;

namespace MultiShop.Web.Areas.Admin.Pages.About
{
    public class IndexModel : PageModel
    {
        private readonly IAboutService _aboutService;

        public IndexModel(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public List<ResultAboutDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Hakkımda";
            ViewData["v3"] = "Hakkımda Listesi";
            ViewData["v0"] = "Hakkımda İşlemleri";
            Items = await _aboutService.GetAllAboutAsync();
        }
    }
}
