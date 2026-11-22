using MultiShop.DtoLayer.IdentityDtos.UserDtos;

namespace MultiShop.Web.Services.UserIdentityServices
{
    public interface IUserIdentityService
    {
        Task<List<ResultUserDto>> GetAllUserListAsync();
    }
}
