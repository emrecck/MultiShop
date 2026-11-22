using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using MultiShop.Web.Services.CargoServices.CargoCustomerServices;

namespace MultiShop.Web.Areas.Admin.Pages.User
{
    public class UserAddressInfoModel : PageModel
    {
        private readonly ICargoCustomerService _cargoCustomerService;

        public UserAddressInfoModel(ICargoCustomerService cargoCustomerService)
        {
            _cargoCustomerService = cargoCustomerService;
        }

        public GetCargoCustomerByIdDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            Input = await _cargoCustomerService.GetByIdCargoCustomerInfoAsync(id);
        }
    }
}
