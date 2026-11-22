using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.ViewComponents.OrderViewComponents
{
    public class _OrderDetailComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
