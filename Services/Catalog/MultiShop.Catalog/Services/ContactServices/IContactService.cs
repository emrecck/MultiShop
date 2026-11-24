using MultiShop.Catalog.Dtos.ContactDtos;

namespace MultiShop.Catalog.Services.ContactServices;

public interface IContactService
{
    Task<IEnumerable<ResultContactDto>> GetAllContactsAsync(CancellationToken cancellationToken);
    Task<GetByIdContactDto?> GetContactByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateContactAsync(CreateContactDto contact, CancellationToken cancellationToken);
    Task<bool> UpdateContactAsync(UpdateContactDto contact, CancellationToken cancellationToken);
    Task<bool> DeleteContactAsync(string id, CancellationToken cancellationToken);
}
