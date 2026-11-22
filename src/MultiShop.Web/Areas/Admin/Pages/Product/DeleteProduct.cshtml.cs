using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CatalogServices.ProductServices;

namespace MultiShop.Web.Areas.Admin.Pages.Product
{
    public class DeleteProductModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteProductModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToPage("/Product/Index", new { area = "Admin" });
        }
    }
}
