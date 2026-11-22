using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.Web.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            var values = await GetBasket();
            var existing = values.BasketItems.FirstOrDefault(x => x.ProductId == basketItemDto.ProductId);
            if (existing != null)
            {
                existing.Quantity += basketItemDto.Quantity;
            }
            else
            {
                values.BasketItems.Add(basketItemDto);
            }

            await SaveBasket(values);
        }

        public async Task DeleteBasket(string userId)
        {
            await _httpClient.DeleteAsync("api/basket");
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/basket");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new BasketTotalDto { BasketItems = new List<BasketItemDto>() };
                }

                var values = await responseMessage.Content.ReadFromJsonAsync<BasketTotalDto>();
                if (values != null)
                {
                    values.BasketItems ??= new List<BasketItemDto>();
                    return values;
                }
            }
            catch
            {
                // Sepet boş veya servis erişilemez olduğunda boş sepet dön
            }

            return new BasketTotalDto { BasketItems = new List<BasketItemDto>() };
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            if (deletedItem == null)
            {
                return false;
            }

            values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _httpClient.PostAsJsonAsync("api/basket", new
            {
                UserId = basketTotalDto.UserId ?? string.Empty,
                basketTotalDto.DiscountCode,
                DiscountRate = basketTotalDto.DiscountRate > 0 ? (decimal?)basketTotalDto.DiscountRate : null,
                BasketItems = basketTotalDto.BasketItems.Select(x => new
                {
                    x.ProductId,
                    x.ProductName,
                    x.ProductImageUrl,
                    x.Quantity,
                    x.Price
                }).ToList()
            });
        }
    }
}
