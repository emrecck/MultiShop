using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Web.Services.CatalogServices.CategoryServices;

namespace MultiShop.Web.Areas.Admin.Pages.Category
{
    public class UpdateCategoryModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public UpdateCategoryDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Kategoriler";
            ViewData["v3"] = "Kategori Listesi";
            ViewData["v0"] = "Kategori İşlemleri";
            Input = await _categoryService.GetByIdCategoryAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _categoryService.UpdateCategoryAsync(Input);
            return RedirectToPage("/Category/Index", new { area = "Admin" });
        }
    }
}
