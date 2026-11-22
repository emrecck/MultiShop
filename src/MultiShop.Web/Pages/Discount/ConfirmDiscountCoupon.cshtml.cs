using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.BasketServices;
using MultiShop.Web.Services.DiscountServices;

namespace MultiShop.Web.Pages.Discount
{
    public class ConfirmDiscountCouponModel : PageModel
    {
        private readonly IDiscountService _discountService;
        private readonly IBasketService _basketService;

        public ConfirmDiscountCouponModel(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string code)
        {
            var discountRate = await _discountService.GetDiscountCouponCountRate(code);

            var basketValues = await _basketService.GetBasket();
            var totalPriceWithTax = basketValues.TotalPrice + basketValues.TotalPrice / 100 * 10;
            var totalNewPriceWithDiscount = totalPriceWithTax - (totalPriceWithTax / 100 * discountRate);

            return RedirectToPage("/ShoppingCart/Index", new { code, discountRate, totalNewPriceWithDiscount });
        }
    }
}
