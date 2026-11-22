using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Web.Services.CatalogServices.CategoryServices;

namespace MultiShop.Web.Areas.Admin.Pages.Category
{
    public class CreateCategoryModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CreateCategoryDto Input { get; set; } = new();

        public void OnGet()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Kategoriler";
            ViewData["v3"] = "Kategori Listesi";
            ViewData["v0"] = "Kategori İşlemleri";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _categoryService.CreateCategoryAsync(Input);
            return RedirectToPage("/Category/Index", new { area = "Admin" });
        }
    }
}
