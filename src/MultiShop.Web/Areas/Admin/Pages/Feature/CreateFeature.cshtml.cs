using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.Web.Services.CatalogServices.FeatureServices;

namespace MultiShop.Web.Areas.Admin.Pages.Feature
{
    public class CreateFeatureModel : PageModel
    {
        private readonly IFeatureService _featureService;

        public CreateFeatureModel(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [BindProperty]
        public CreateFeatureDto Input { get; set; } = new();

        public void OnGet()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Öne Çıkan Alanlar";
            ViewData["v3"] = "Öne Çıkan Alan Listesi";
            ViewData["v0"] = "Ana Sayfa Öne Çıkan Alan İşlemleri";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _featureService.CreateFeatureAsync(Input);
            return RedirectToPage("/Feature/Index", new { area = "Admin" });
        }
    }
}
