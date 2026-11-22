using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MultiShop.Web.Pages.Payment
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["directory1"] = "MultiShop";
            ViewData["directory2"] = "Ödeme Ekranı";
            ViewData["directory3"] = "Kartla Ödeme";
        }
    }
}
