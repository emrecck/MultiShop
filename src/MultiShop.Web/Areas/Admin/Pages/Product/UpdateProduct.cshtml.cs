using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Web.Services.CatalogServices.CategoryServices;
using MultiShop.Web.Services.CatalogServices.ProductServices;

namespace MultiShop.Web.Areas.Admin.Pages.Product
{
    public class UpdateProductModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public UpdateProductModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public UpdateProductDto Input { get; set; } = new();

        public List<SelectListItem> CategoryValues { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Ürünler";
            ViewData["v3"] = "Ürün Listesi";
            ViewData["v0"] = "Ürün İşlemleri";

            var values = await _categoryService.GetAllCategoryAsync();
            CategoryValues = values.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryID
            }).ToList();

            Input = await _productService.GetByIdProductAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _productService.UpdateProductAsync(Input);
            return RedirectToPage("/Product/Index", new { area = "Admin" });
        }
    }
}
