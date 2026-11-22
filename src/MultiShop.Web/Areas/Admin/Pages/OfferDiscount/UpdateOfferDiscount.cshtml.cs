using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.Web.Services.CatalogServices.OfferDiscountServices;

namespace MultiShop.Web.Areas.Admin.Pages.OfferDiscount
{
    [AllowAnonymous]
    public class UpdateOfferDiscountModel : PageModel
    {
        private readonly IOfferDiscountService _offerDiscountService;

        public UpdateOfferDiscountModel(IOfferDiscountService offerDiscountService)
        {
            _offerDiscountService = offerDiscountService;
        }

        [BindProperty]
        public UpdateOfferDiscountDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "İndirim Teklifleri";
            ViewData["v3"] = "İndirim Teklif Listesi";
            ViewData["v0"] = "İndirim Teklif İşlemleri";
            Input = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _offerDiscountService.UpdateOfferDiscountAsync(Input);
            return RedirectToPage("/OfferDiscount/Index", new { area = "Admin" });
        }
    }
}
