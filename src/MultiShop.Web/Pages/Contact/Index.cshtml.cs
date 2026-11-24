using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using MultiShop.Web.Services.CatalogServices.ContactServices;

namespace MultiShop.Web.Pages.Contact
{
    public class IndexModel : PageModel
    {
        private readonly IContactService _contactService;

        public IndexModel(IContactService contactService)
        {
            _contactService = contactService;
        }

        [BindProperty]
        public CreateContactDto Input { get; set; } = new();

        public void OnGet()
        {
            ViewData["directory1"] = "MultiShop";
            ViewData["directory2"] = "İletişim";
            ViewData["directory3"] = "Mesaj Gönder";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Input.IsRead = false;
            Input.SendDate = DateTime.Now;
            await _contactService.CreateContactAsync(Input);
            return RedirectToPage("/");
        }
    }
}
