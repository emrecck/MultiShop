using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CatalogServices.FeatureSliderServices;

namespace MultiShop.Web.Areas.Admin.Pages.FeatureSlider
{
    public class DeleteFeatureSliderModel : PageModel
    {
        private readonly IFeatureSliderService _featureSliderService;

        public DeleteFeatureSliderModel(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return RedirectToPage("/FeatureSlider/Index", new { area = "Admin" });
        }
    }
}
