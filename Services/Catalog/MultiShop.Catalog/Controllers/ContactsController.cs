using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Services.ContactServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("api/contacts")]
public sealed class ContactsController(IContactService contactService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultContactDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await contactService.GetAllContactsAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdContactDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var item = await contactService.GetContactByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContactDto contact, CancellationToken cancellationToken)
    {
        var id = await contactService.CreateContactAsync(contact, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateContactDto contact,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, contact.ContactId, StringComparison.OrdinalIgnoreCase))
            return BadRequest();
        var updated = await contactService.UpdateContactAsync(contact, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
        => await contactService.DeleteContactAsync(id, cancellationToken) ? NoContent() : NotFound();
}
