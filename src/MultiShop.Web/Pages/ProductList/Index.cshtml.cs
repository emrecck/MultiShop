using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MultiShop.Web.Pages.ProductList
{
    public class IndexModel : PageModel
    {
        public void OnGet(string? id)
        {
            ViewData["directory1"] = "Ana Sayfa";
            ViewData["directory2"] = "Ürünler";
            ViewData["directory3"] = "Ürün Listesi";
            ViewData["i"] = id;
        }
    }
}
