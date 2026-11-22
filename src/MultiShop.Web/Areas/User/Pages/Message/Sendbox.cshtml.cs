using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.MessageDtos;
using MultiShop.Web.Services.Interfaces;
using MultiShop.Web.Services.MessageServices;

namespace MultiShop.Web.Areas.User.Pages.Message
{
    public class SendboxModel : PageModel
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public SendboxModel(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public List<ResultSendboxMessageDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userService.GetUserInfo();
            Items = await _messageService.GetSendboxMessageAsync(user.Id);
        }
    }
}
