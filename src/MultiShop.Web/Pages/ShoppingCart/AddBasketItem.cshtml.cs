using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.Web.Services.BasketServices;
using MultiShop.Web.Services.CatalogServices.ProductServices;

namespace MultiShop.Web.Pages.ShoppingCart
{
    public class AddBasketItemModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public AddBasketItemModel(IProductService productService, IBasketService basketService)
        {
            _productService = productService;
            _basketService = basketService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var values = await _productService.GetByIdProductAsync(id);
            var item = new BasketItemDto
            {
                ProductId = values.ProductId,
                ProductName = values.ProductName,
                Price = values.ProductPrice,
                Quantity = 1,
                ProductImageUrl = values.ProductImageUrl
            };
            await _basketService.AddBasketItem(item);
            return RedirectToPage("/ShoppingCart/Index");
        }
    }
}
