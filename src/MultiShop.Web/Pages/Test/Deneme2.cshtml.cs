using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Web.Services.CatalogServices.CategoryServices;

namespace MultiShop.Web.Pages.Test
{
    public class Deneme2Model : PageModel
    {
        private readonly ICategoryService _categoryService;

        public Deneme2Model(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<ResultCategoryDto> Categories { get; set; } = new();

        public async Task OnGetAsync()
        {
            Categories = await _categoryService.GetAllCategoryAsync();
        }
    }
}
