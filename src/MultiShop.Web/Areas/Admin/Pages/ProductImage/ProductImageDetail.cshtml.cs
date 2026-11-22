using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.Web.Services.CatalogServices.ProductImageServices;

namespace MultiShop.Web.Areas.Admin.Pages.ProductImage
{
    [AllowAnonymous]
    public class ProductImageDetailModel : PageModel
    {
        private readonly IProductImageService _productImageService;

        public ProductImageDetailModel(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [BindProperty]
        public UpdateProductImageDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Ürünler";
            ViewData["v3"] = "Ürün Görsel Güncelleme Sayfası";
            ViewData["v0"] = "Ürün Görsel İşlemleri";

            var images = await _productImageService.GetByProductIdProductImageAsync(id);
            Input = new UpdateProductImageDto
            {
                ProductImageID = images.ProductImageID,
                Image1 = images.Image1,
                Image2 = images.Image2,
                Image3 = images.Image3,
                Image4 = images.Image4,
                ProductId = string.IsNullOrEmpty(images.ProductId) ? id : images.ProductId
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _productImageService.UpdateProductImageAsync(Input);
            return RedirectToPage("/Product/ProductListWithCategory", new { area = "Admin" });
        }
    }
}
