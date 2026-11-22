using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.Web.Services.CatalogServices.ProductDetailServices;

namespace MultiShop.Web.Areas.Admin.Pages.ProductDetail
{
    [AllowAnonymous]
    public class UpdateProductDetailModel : PageModel
    {
        private readonly IProductDetailService _productDetailService;

        public UpdateProductDetailModel(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [BindProperty]
        public UpdateProductDetailDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Ürünler";
            ViewData["v3"] = "Ürün Açıklama ve Bilgi Güncelleme Sayfası";
            ViewData["v0"] = "Ürün İşlemleri";

            var detail = await _productDetailService.GetByProductIdProductDetailAsync(id);
            Input = new UpdateProductDetailDto
            {
                ProductDetailId = detail.ProductDetailId,
                ProductDescription = detail.ProductDescription,
                ProductInfo = detail.ProductInfo,
                ProductId = string.IsNullOrEmpty(detail.ProductId) ? id : detail.ProductId
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _productDetailService.UpdateProductDetailAsync(Input);
            return RedirectToPage("/Product/ProductListWithCategory", new { area = "Admin" });
        }
    }
}
