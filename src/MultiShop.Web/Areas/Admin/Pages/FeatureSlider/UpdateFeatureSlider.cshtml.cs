using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Web.Services.CatalogServices.FeatureSliderServices;

namespace MultiShop.Web.Areas.Admin.Pages.FeatureSlider
{
    public class UpdateFeatureSliderModel : PageModel
    {
        private readonly IFeatureSliderService _featureSliderService;

        public UpdateFeatureSliderModel(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        [BindProperty]
        public UpdateFeatureSliderDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Öne Çıkan Görseller";
            ViewData["v3"] = "Slider Öne Çıkan Görsel Listesi";
            ViewData["v0"] = "Öne Çıkan Slider Görsel İşlemleri";
            Input = await _featureSliderService.GetByIdFeatureSliderAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _featureSliderService.UpdateFeatureSliderAsync(Input);
            return RedirectToPage("/FeatureSlider/Index", new { area = "Admin" });
        }
    }
}
