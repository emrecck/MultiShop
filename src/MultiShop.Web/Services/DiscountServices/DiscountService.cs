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
                var responseMessage = await _httpClient.GetAsync("api/coupons");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new GetDiscountCodeDetailByCode();
                }

                var coupons = await responseMessage.Content.ReadFromJsonAsync<List<CouponApiDto>>();
                var match = coupons?.FirstOrDefault(x =>
                    x.IsActive &&
                    x.ExpirationDate >= DateTimeOffset.UtcNow &&
                    string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));

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
            var detail = await GetDiscountCode(code);
            return detail.Rate;
        }

        private sealed record CouponApiDto(
            int CouponId,
            string Code,
            decimal DiscountAmount,
            DateTimeOffset ExpirationDate,
            bool IsActive);
    }
}
