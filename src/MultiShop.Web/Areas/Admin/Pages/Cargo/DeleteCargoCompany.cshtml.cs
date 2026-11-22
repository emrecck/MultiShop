using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CargoServices.CargoCompanyServices;

namespace MultiShop.Web.Areas.Admin.Pages.Cargo
{
    public class DeleteCargoCompanyModel : PageModel
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public DeleteCargoCompanyModel(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await _cargoCompanyService.DeleteCargoCompanyAsync(id);
            return RedirectToPage("/Cargo/CargoCompanyList", new { area = "Admin" });
        }
    }
}
