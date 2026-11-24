using MultiShop.Catalog.Dtos.SpecialOfferDtos;

namespace MultiShop.Catalog.Services.SpecialOfferServices;

public interface ISpecialOfferService
{
    Task<IEnumerable<ResultSpecialOfferDto>> GetAllSpecialOffersAsync(CancellationToken cancellationToken);
    Task<GetByIdSpecialOfferDto?> GetSpecialOfferByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateSpecialOfferAsync(CreateSpecialOfferDto specialOffer, CancellationToken cancellationToken);
    Task<bool> UpdateSpecialOfferAsync(UpdateSpecialOfferDto specialOffer, CancellationToken cancellationToken);
    Task<bool> DeleteSpecialOfferAsync(string id, CancellationToken cancellationToken);
}
