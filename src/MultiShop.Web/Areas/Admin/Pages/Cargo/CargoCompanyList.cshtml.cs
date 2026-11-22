using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using MultiShop.Web.Services.CargoServices.CargoCompanyServices;

namespace MultiShop.Web.Areas.Admin.Pages.Cargo
{
    public class CargoCompanyListModel : PageModel
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public CargoCompanyListModel(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        public List<ResultCargoCompanyDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await _cargoCompanyService.GetAllCargoCompanyAsync();
        }
    }
}
