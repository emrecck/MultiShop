using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.ViewComponents.ProductListViewComponents
{
    public class _ProductListGetProductCountComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
