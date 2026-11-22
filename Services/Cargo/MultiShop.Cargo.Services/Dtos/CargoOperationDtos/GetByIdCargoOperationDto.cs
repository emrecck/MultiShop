namespace MultiShop.Cargo.Services.Dtos.CargoOperationDtos;

public class GetByIdCargoOperationDto
{
    public int Id { get; set; }
    public required string Location { get; set; }
    public required string TargetLocation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
