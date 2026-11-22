using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.Web.Services.Interfaces;

namespace MultiShop.Web.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IIdentityService _identityService;

        public IndexModel(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [BindProperty]
        public SignInDto Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _identityService.SignIn(Input);
            return RedirectToPage("/User/Index");
        }
    }
}
