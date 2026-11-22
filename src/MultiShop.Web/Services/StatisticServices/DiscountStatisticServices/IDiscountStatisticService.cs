namespace MultiShop.Web.Services.StatisticServices.DiscountStatisticServices
{
    public interface IDiscountStatisticService
    {
        Task<int> GetDiscountCouponCount();
    }
}
