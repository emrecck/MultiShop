using System;

namespace MultiShop.Cargo.Domain.Entites;

public class CargoCustomer : BaseEntity<int>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
}
