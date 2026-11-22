using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.MessageDtos;
using MultiShop.Web.Services.Interfaces;
using MultiShop.Web.Services.MessageServices;

namespace MultiShop.Web.Areas.User.Pages.Message
{
    public class InboxModel : PageModel
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public InboxModel(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public List<ResultInboxMessageDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userService.GetUserInfo();
            Items = await _messageService.GetInboxMessageAsync(user.Id);
        }
    }
}
