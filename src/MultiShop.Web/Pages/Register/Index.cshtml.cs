using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using MultiShop.Web.Settings;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.Web.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IndexModel(IHttpClientFactory httpClientFactory, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        [BindProperty]
        public CreateRegisterDto Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Input.Password == Input.ConfirmPassword)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(Input);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var baseUrl = _serviceApiSettings.IdentityServerUrl.TrimEnd('/');
                var responseMessage = await client.PostAsync($"{baseUrl}/api/Registers", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Login/Index");
                }
            }
            return Page();
        }
    }
}
