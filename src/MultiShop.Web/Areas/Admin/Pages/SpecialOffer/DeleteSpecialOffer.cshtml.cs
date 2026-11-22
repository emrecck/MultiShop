using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CatalogServices.SpecialOfferServices;

namespace MultiShop.Web.Areas.Admin.Pages.SpecialOffer
{
    public class DeleteSpecialOfferModel : PageModel
    {
        private readonly ISpecialOfferService _specialOfferService;

        public DeleteSpecialOfferModel(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return RedirectToPage("/SpecialOffer/Index", new { area = "Admin" });
        }
    }
}
