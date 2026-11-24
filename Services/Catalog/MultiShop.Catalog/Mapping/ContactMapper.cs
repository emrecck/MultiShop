using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class ContactMapper
{
    [MapperIgnoreTarget(nameof(Contact.ContactId))]
    public partial Contact ToEntity(CreateContactDto source);
    public partial Contact ToEntity(UpdateContactDto source);
    public partial GetByIdContactDto ToGetByIdDto(Contact source);
    public partial ResultContactDto ToResultDto(Contact source);
}
