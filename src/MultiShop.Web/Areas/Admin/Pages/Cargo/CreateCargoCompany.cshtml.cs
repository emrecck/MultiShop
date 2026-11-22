using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using MultiShop.Web.Services.CargoServices.CargoCompanyServices;

namespace MultiShop.Web.Areas.Admin.Pages.Cargo
{
    public class CreateCargoCompanyModel : PageModel
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public CreateCargoCompanyModel(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        [BindProperty]
        public CreateCargoCompanyDto Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _cargoCompanyService.CreateCargoCompanyAsync(Input);
            return RedirectToPage("/Cargo/CargoCompanyList", new { area = "Admin" });
        }
    }
}
