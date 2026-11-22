namespace MultiShop.Cargo.Services.Dtos.CargoOperationDtos;

public class CreateCargoOperationDto
{
    public required string Location { get; set; }
    public required string TargetLocation { get; set; }
}
