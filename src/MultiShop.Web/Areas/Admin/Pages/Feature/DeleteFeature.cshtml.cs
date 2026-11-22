using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CatalogServices.FeatureServices;

namespace MultiShop.Web.Areas.Admin.Pages.Feature
{
    public class DeleteFeatureModel : PageModel
    {
        private readonly IFeatureService _featureService;

        public DeleteFeatureModel(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _featureService.DeleteFeatureAsync(id);
            return RedirectToPage("/Feature/Index", new { area = "Admin" });
        }
    }
}
