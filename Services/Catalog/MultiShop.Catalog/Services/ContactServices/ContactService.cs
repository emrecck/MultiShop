using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ContactServices;

public class ContactService : IContactService
{
    private readonly IMongoCollection<Contact> _contacts;
    private readonly ContactMapper _mapper;

    public ContactService(ContactMapper mapper, IOptions<DatabaseSettings> databaseSettingsOptions)
    {
        _mapper = mapper;
        var settings = databaseSettingsOptions.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        _contacts = db.GetCollection<Contact>(settings.ContactCollectionName);
    }

    public async Task<string> CreateContactAsync(CreateContactDto contact, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(contact);
        await _contacts.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity.ContactId;
    }

    public async Task<bool> DeleteContactAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _contacts.DeleteOneAsync(x => x.ContactId == id, cancellationToken);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<ResultContactDto>> GetAllContactsAsync(CancellationToken cancellationToken)
    {
        var items = await _contacts.Find(_ => true).ToListAsync(cancellationToken);
        return items.Select(_mapper.ToResultDto);
    }

    public async Task<GetByIdContactDto?> GetContactByIdAsync(string id, CancellationToken cancellationToken)
    {
        var item = await _contacts.Find(x => x.ContactId == id).FirstOrDefaultAsync(cancellationToken);
        return item is null ? null : _mapper.ToGetByIdDto(item);
    }

    public async Task<bool> UpdateContactAsync(UpdateContactDto contact, CancellationToken cancellationToken)
    {
        var filter = Builders<Contact>.Filter.Eq(x => x.ContactId, contact.ContactId);
        var update = Builders<Contact>.Update
            .Set(x => x.NameSurname, contact.NameSurname)
            .Set(x => x.Email, contact.Email)
            .Set(x => x.Subject, contact.Subject)
            .Set(x => x.Message, contact.Message)
            .Set(x => x.IsRead, contact.IsRead)
            .Set(x => x.SendDate, contact.SendDate);
        var result = await _contacts.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.MatchedCount > 0;
    }
}
