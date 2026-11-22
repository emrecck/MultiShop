namespace MultiShop.Cargo.Services.Dtos.CargoCompanyDtos;

public class CreateCargoCompanyDto
{
    public required string CompanyName { get; set; }
    public required string CompanyEmail { get; set; }
    public required string CompanyPhoneNumber { get; set; }
    public string? CompanyAddress { get; set; }
}
