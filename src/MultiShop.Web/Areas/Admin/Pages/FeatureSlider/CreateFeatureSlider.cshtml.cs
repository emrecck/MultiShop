using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Web.Services.CatalogServices.FeatureSliderServices;

namespace MultiShop.Web.Areas.Admin.Pages.FeatureSlider
{
    public class CreateFeatureSliderModel : PageModel
    {
        private readonly IFeatureSliderService _featureSliderService;

        public CreateFeatureSliderModel(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        [BindProperty]
        public CreateFeatureSliderDto Input { get; set; } = new();

        public void OnGet()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Öne Çıkan Görseller";
            ViewData["v3"] = "Slider Öne Çıkan Görsel Listesi";
            ViewData["v0"] = "Öne Çıkan Slider Görsel İşlemleri";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _featureSliderService.CreateFeatureSliderAsync(Input);
            return RedirectToPage("/FeatureSlider/Index", new { area = "Admin" });
        }
    }
}
