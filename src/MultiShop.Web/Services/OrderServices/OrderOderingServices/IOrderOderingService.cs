using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;

namespace MultiShop.Web.Services.OrderServices.OrderOderingServices
{
    public interface IOrderOderingService
    {
        Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string id);
    }
}
