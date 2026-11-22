using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CatalogServices.CategoryServices;

namespace MultiShop.Web.Areas.Admin.Pages.Category
{
    public class DeleteCategoryModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToPage("/Category/Index", new { area = "Admin" });
        }
    }
}
