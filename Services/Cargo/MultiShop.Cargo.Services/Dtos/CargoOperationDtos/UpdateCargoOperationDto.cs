namespace MultiShop.Cargo.Services.Dtos.CargoOperationDtos;

public class UpdateCargoOperationDto
{
    public int Id { get; set; }
    public required string Location { get; set; }
    public required string TargetLocation { get; set; }
    public bool IsActive { get; set; } = true;
}
