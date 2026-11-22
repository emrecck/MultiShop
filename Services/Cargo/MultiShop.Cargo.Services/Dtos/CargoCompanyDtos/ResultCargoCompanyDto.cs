namespace MultiShop.Cargo.Services.Dtos.CargoCompanyDtos;

public class ResultCargoCompanyDto
{
    public int Id { get; set; }
    public required string CompanyName { get; set; }
    public required string CompanyEmail { get; set; }
    public required string CompanyPhoneNumber { get; set; }
    public string? CompanyAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
