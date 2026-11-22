using MultiShop.DtoLayer.IdentityDtos.LoginDtos;

namespace MultiShop.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> SignIn(SignInDto signInDto);
        Task<bool> GetRefreshToken();
    }
}
