using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.BasketServices;

namespace MultiShop.Web.Pages.ShoppingCart
{
    public class RemoveBasketItemModel : PageModel
    {
        private readonly IBasketService _basketService;

        public RemoveBasketItemModel(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _basketService.RemoveBasketItem(id);
            return RedirectToPage("/ShoppingCart/Index");
        }
    }
}
