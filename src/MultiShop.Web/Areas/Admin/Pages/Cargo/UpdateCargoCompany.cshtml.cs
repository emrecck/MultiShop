using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using MultiShop.Web.Services.CargoServices.CargoCompanyServices;

namespace MultiShop.Web.Areas.Admin.Pages.Cargo
{
    public class UpdateCargoCompanyModel : PageModel
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public UpdateCargoCompanyModel(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        [BindProperty]
        public UpdateCargoCompanyDto Input { get; set; } = new();

        public async Task OnGetAsync(int id)
        {
            Input = await _cargoCompanyService.GetByIdCargoCompanyAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _cargoCompanyService.UpdateCargoCompanyAsync(Input);
            return RedirectToPage("/Cargo/CargoCompanyList", new { area = "Admin" });
        }
    }
}
