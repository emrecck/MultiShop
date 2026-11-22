using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Web.Services.CatalogServices.CategoryServices;

namespace MultiShop.Web.Areas.Admin.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<ResultCategoryDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Kategoriler";
            ViewData["v3"] = "Kategori Listesi";
            ViewData["v0"] = "Kategori İşlemleri";
            Items = await _categoryService.GetAllCategoryAsync();
        }
    }
}
