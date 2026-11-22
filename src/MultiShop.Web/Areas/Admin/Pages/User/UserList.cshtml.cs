using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.Web.Services.UserIdentityServices;

namespace MultiShop.Web.Areas.Admin.Pages.User
{
    public class UserListModel : PageModel
    {
        private readonly IUserIdentityService _userIdentityService;

        public UserListModel(IUserIdentityService userIdentityService)
        {
            _userIdentityService = userIdentityService;
        }

        public List<ResultUserDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await _userIdentityService.GetAllUserListAsync();
        }
    }
}
