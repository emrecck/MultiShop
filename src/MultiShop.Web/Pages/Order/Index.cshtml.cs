using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;
using MultiShop.Web.Services.Interfaces;
using MultiShop.Web.Services.OrderServices.OrderAddressServices;

namespace MultiShop.Web.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderAddressService _orderAddressService;
        private readonly IUserService _userService;

        public IndexModel(IOrderAddressService orderAddressService, IUserService userService)
        {
            _orderAddressService = orderAddressService;
            _userService = userService;
        }

        [BindProperty]
        public CreateOrderAddressDto Input { get; set; } = new();

        public void OnGet()
        {
            ViewData["directory1"] = "MultiShop";
            ViewData["directory2"] = "Siparişler";
            ViewData["directory3"] = "Sipariş İşlemleri";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var values = await _userService.GetUserInfo();
            Input.UserId = values.Id;
            Input.Description = "aa";

            await _orderAddressService.CreateOrderAddressAsync(Input);

            return RedirectToPage("/Payment/Index");
        }
    }
}
