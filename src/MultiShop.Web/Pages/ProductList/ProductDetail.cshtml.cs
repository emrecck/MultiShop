using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MultiShop.Web.Pages.ProductList
{
    public class ProductDetailModel : PageModel
    {
        public void OnGet(string? id)
        {
            ViewData["directory1"] = "Ana Sayfa";
            ViewData["directory2"] = "Ürün Listesi";
            ViewData["directory3"] = "Ürün Detayları";
            ViewData["x"] = id;
        }
    }
}
