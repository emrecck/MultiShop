using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.Web.Services.CatalogServices.OfferDiscountServices;

namespace MultiShop.Web.Areas.Admin.Pages.OfferDiscount
{
    [AllowAnonymous]
    public class CreateOfferDiscountModel : PageModel
    {
        private readonly IOfferDiscountService _offerDiscountService;

        public CreateOfferDiscountModel(IOfferDiscountService offerDiscountService)
        {
            _offerDiscountService = offerDiscountService;
        }

        [BindProperty]
        public CreateOfferDiscountDto Input { get; set; } = new();

        public void OnGet()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "İndirim Teklifleri";
            ViewData["v3"] = "İndirim Teklif Listesi";
            ViewData["v0"] = "İndirim Teklif İşlemleri";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _offerDiscountService.CreateOfferDiscountAsync(Input);
            return RedirectToPage("/OfferDiscount/Index", new { area = "Admin" });
        }
    }
}
