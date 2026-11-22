using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CatalogServices.OfferDiscountServices;

namespace MultiShop.Web.Areas.Admin.Pages.OfferDiscount
{
    [AllowAnonymous]
    public class DeleteOfferDiscountModel : PageModel
    {
        private readonly IOfferDiscountService _offerDiscountService;

        public DeleteOfferDiscountModel(IOfferDiscountService offerDiscountService)
        {
            _offerDiscountService = offerDiscountService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _offerDiscountService.DeleteOfferDiscountAsync(id);
            return RedirectToPage("/OfferDiscount/Index", new { area = "Admin" });
        }
    }
}
