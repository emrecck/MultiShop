using MultiShop.Catalog.Dtos.OfferDiscountDtos;

namespace MultiShop.Catalog.Services.OfferDiscountServices;

public interface IOfferDiscountService
{
    Task<IEnumerable<ResultOfferDiscountDto>> GetAllOfferDiscountsAsync(CancellationToken cancellationToken);
    Task<GetByIdOfferDiscountDto?> GetOfferDiscountByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateOfferDiscountAsync(CreateOfferDiscountDto offerDiscount, CancellationToken cancellationToken);
    Task<bool> UpdateOfferDiscountAsync(UpdateOfferDiscountDto offerDiscount, CancellationToken cancellationToken);
    Task<bool> DeleteOfferDiscountAsync(string id, CancellationToken cancellationToken);
}
