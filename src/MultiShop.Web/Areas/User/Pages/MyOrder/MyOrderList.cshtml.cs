using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using MultiShop.Web.Services.Interfaces;
using MultiShop.Web.Services.OrderServices.OrderOderingServices;

namespace MultiShop.Web.Areas.User.Pages.MyOrder
{
    public class MyOrderListModel : PageModel
    {
        private readonly IOrderOderingService _orderOderingService;
        private readonly IUserService _userService;

        public MyOrderListModel(IOrderOderingService orderOderingService, IUserService userService)
        {
            _orderOderingService = orderOderingService;
            _userService = userService;
        }

        public List<ResultOrderingByUserIdDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userService.GetUserInfo();
            Items = await _orderOderingService.GetOrderingByUserId(user.Id);
        }
    }
}
