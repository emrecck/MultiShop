using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.Web.Services.CatalogServices.SpecialOfferServices;

namespace MultiShop.Web.Areas.Admin.Pages.SpecialOffer
{
    public class CreateSpecialOfferModel : PageModel
    {
        private readonly ISpecialOfferService _specialOfferService;

        public CreateSpecialOfferModel(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [BindProperty]
        public CreateSpecialOfferDto Input { get; set; } = new();

        public void OnGet()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Özel Teklifler";
            ViewData["v3"] = "Özel Teklif ve Günün İndirim Listesi";
            ViewData["v0"] = "Kategori İşlemleri";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _specialOfferService.CreateSpecialOfferAsync(Input);
            return RedirectToPage("/SpecialOffer/Index", new { area = "Admin" });
        }
    }
}
