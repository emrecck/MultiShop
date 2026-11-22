namespace MultiShop.Cargo.Services.Dtos.CargoReceiverCustomerDtos;

public class ResultCargoReceiverCustomerDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
