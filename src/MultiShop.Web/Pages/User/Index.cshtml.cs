using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Models;
using MultiShop.Web.Services.Interfaces;

namespace MultiShop.Web.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserDetailViewModel UserDetail { get; set; } = new();

        public async Task OnGetAsync()
        {
            UserDetail = await _userService.GetUserInfo();
        }
    }
}
