using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.Web.Services.CatalogServices.FeatureServices;

namespace MultiShop.Web.Areas.Admin.Pages.Feature
{
    public class UpdateFeatureModel : PageModel
    {
        private readonly IFeatureService _featureService;

        public UpdateFeatureModel(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [BindProperty]
        public UpdateFeatureDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Öne Çıkan Alanlar";
            ViewData["v3"] = "Öne Çıkan Alan Listesi";
            ViewData["v0"] = "Ana Sayfa Öne Çıkan Alan İşlemleri";
            Input = await _featureService.GetByIdFeatureAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _featureService.UpdateFeatureAsync(Input);
            return RedirectToPage("/Feature/Index", new { area = "Admin" });
        }
    }
}
