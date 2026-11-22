using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.Web.Services.CatalogServices.SpecialOfferServices;

namespace MultiShop.Web.Areas.Admin.Pages.SpecialOffer
{
    public class UpdateSpecialOfferModel : PageModel
    {
        private readonly ISpecialOfferService _specialOfferService;

        public UpdateSpecialOfferModel(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [BindProperty]
        public UpdateSpecialOfferDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Özel Teklifler";
            ViewData["v3"] = "Özel Teklif ve Günün İndirim Listesi";
            ViewData["v0"] = "Kategori İşlemleri";
            Input = await _specialOfferService.GetByIdSpecialOfferAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _specialOfferService.UpdateSpecialOfferAsync(Input);
            return RedirectToPage("/SpecialOffer/Index", new { area = "Admin" });
        }
    }
}
