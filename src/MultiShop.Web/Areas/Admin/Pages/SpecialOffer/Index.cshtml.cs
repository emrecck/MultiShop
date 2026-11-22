using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.Web.Services.CatalogServices.SpecialOfferServices;

namespace MultiShop.Web.Areas.Admin.Pages.SpecialOffer
{
    public class IndexModel : PageModel
    {
        private readonly ISpecialOfferService _specialOfferService;

        public IndexModel(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        public List<ResultSpecialOfferDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Özel Teklifler";
            ViewData["v3"] = "Özel Teklif ve Günün İndirim Listesi";
            ViewData["v0"] = "Kategori İşlemleri";
            Items = await _specialOfferService.GetAllSpecialOfferAsync();
        }
    }
}
