using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MultiShop.Web.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["directory1"] = "MultiShop";
            ViewData["directory2"] = "Ana Sayfa";
            ViewData["directory3"] = "Ürün Listesi";
        }
    }
}
