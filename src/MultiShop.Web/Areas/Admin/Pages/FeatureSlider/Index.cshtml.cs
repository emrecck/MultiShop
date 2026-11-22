using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Web.Services.CatalogServices.FeatureSliderServices;

namespace MultiShop.Web.Areas.Admin.Pages.FeatureSlider
{
    public class IndexModel : PageModel
    {
        private readonly IFeatureSliderService _featureSliderService;

        public IndexModel(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        public List<ResultFeatureSliderDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Öne Çıkan Görseller";
            ViewData["v3"] = "Slider Öne Çıkan Görsel Listesi";
            ViewData["v0"] = "Öne Çıkan Slider Görsel İşlemleri";
            Items = await _featureSliderService.GetAllFeatureSliderAsync();
        }
    }
}
