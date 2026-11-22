using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;

namespace MultiShop.Web.Services.CargoServices.CargoCustomerServices
{
    public interface ICargoCustomerService
    {
        Task<GetCargoCustomerByIdDto> GetByIdCargoCustomerInfoAsync(string id);
    }
}
