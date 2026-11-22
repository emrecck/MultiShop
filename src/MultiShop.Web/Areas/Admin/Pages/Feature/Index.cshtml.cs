using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.Web.Services.CatalogServices.FeatureServices;

namespace MultiShop.Web.Areas.Admin.Pages.Feature
{
    public class IndexModel : PageModel
    {
        private readonly IFeatureService _featureService;

        public IndexModel(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public List<ResultFeatureDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Öne Çıkan Alanlar";
            ViewData["v3"] = "Öne Çıkan Alan Listesi";
            ViewData["v0"] = "Ana Sayfa Öne Çıkan Alan İşlemleri";
            Items = await _featureService.GetAllFeatureAsync();
        }
    }
}
