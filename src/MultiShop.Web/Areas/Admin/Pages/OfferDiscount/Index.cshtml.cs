using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.Web.Services.CatalogServices.OfferDiscountServices;

namespace MultiShop.Web.Areas.Admin.Pages.OfferDiscount
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly IOfferDiscountService _offerDiscountService;

        public IndexModel(IOfferDiscountService offerDiscountService)
        {
            _offerDiscountService = offerDiscountService;
        }

        public List<ResultOfferDiscountDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "İndirim Teklifleri";
            ViewData["v3"] = "İndirim Teklif Listesi";
            ViewData["v0"] = "İndirim Teklif İşlemleri";
            Items = await _offerDiscountService.GetAllOfferDiscountAsync();
        }
    }
}
