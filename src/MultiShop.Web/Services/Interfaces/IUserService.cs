using MultiShop.Web.Models;

namespace MultiShop.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailViewModel> GetUserInfo();
    }
}
