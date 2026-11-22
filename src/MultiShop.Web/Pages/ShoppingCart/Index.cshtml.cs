using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.BasketServices;

namespace MultiShop.Web.Pages.ShoppingCart
{
    public class IndexModel : PageModel
    {
        private readonly IBasketService _basketService;

        public IndexModel(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task OnGetAsync(string? code, int discountRate, decimal totalNewPriceWithDiscount)
        {
            ViewData["code"] = code;
            ViewData["discountRate"] = discountRate;
            ViewData["totalNewPriceWithDiscount"] = totalNewPriceWithDiscount;
            ViewData["directory1"] = "Ana Sayfa";
            ViewData["directory2"] = "Ürünler";
            ViewData["directory3"] = "Sepetim";

            var values = await _basketService.GetBasket();
            ViewData["total"] = values.TotalPrice;
            var tax = values.TotalPrice / 100 * 10;
            var totalPriceWithTax = values.TotalPrice + tax;
            ViewData["totalPriceWithTax"] = totalPriceWithTax;
            ViewData["tax"] = tax;
        }
    }
}
