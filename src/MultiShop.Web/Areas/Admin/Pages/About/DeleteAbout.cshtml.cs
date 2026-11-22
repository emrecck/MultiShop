using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CatalogServices.AboutServices;

namespace MultiShop.Web.Areas.Admin.Pages.About
{
    public class DeleteAboutModel : PageModel
    {
        private readonly IAboutService _aboutService;

        public DeleteAboutModel(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _aboutService.DeleteAboutAsync(id);
            return RedirectToPage("/About/Index", new { area = "Admin" });
        }
    }
}
