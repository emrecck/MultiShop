using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.Web.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetDiscountCodeDetailByCode> GetDiscountCode(string code)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/coupons/by-code/{Uri.EscapeDataString(code)}");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new GetDiscountCodeDetailByCode();
                }

                var match = await responseMessage.Content.ReadFromJsonAsync<CouponApiDto>();
                if (match == null)
                {
                    return new GetDiscountCodeDetailByCode();
                }

                return new GetDiscountCodeDetailByCode
                {
                    CouponId = match.CouponId,
                    Code = match.Code,
                    Rate = (int)match.DiscountAmount,
                    IsActive = match.IsActive,
                    ValidDate = match.ExpirationDate.UtcDateTime
                };
            }
            catch
            {
                return new GetDiscountCodeDetailByCode();
            }
        }

        public async Task<int> GetDiscountCouponCountRate(string code)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/coupons/rate/{Uri.EscapeDataString(code)}");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return 0;
                }

                var rate = await responseMessage.Content.ReadFromJsonAsync<decimal>();
                return (int)rate;
            }
            catch
            {
                return 0;
            }
        }

        private sealed record CouponApiDto(
            int CouponId,
            string Code,
            decimal DiscountAmount,
            DateTimeOffset ExpirationDate,
            bool IsActive);
    }
}
