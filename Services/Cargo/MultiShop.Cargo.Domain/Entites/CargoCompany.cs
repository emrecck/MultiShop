using System;

namespace MultiShop.Cargo.Domain.Entites;

public class CargoCompany : BaseEntity<int>, IEntity<int>
{
    public required string CompanyName { get; set; }
    public required string CompanyEmail { get; set; }
    public required string CompanyPhoneNumber { get; set; }
    public string? CompanyAddress { get; set; }
}
