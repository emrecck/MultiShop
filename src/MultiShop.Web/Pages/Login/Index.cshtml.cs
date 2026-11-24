using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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

        public IActionResult OnGet(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return LocalRedirect(returnUrl ?? "/");
            }

            return Page();

            return Challenge(
                new AuthenticationProperties { RedirectUri = returnUrl ?? "/" },
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _identityService.SignIn(Input);
            return RedirectToPage("/User/Index");
        }
    }
}
