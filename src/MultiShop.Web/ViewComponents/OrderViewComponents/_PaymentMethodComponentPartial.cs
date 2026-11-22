using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.ViewComponents.OrderViewComponents
{
    public class _PaymentMethodComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
