namespace MultiShop.Cargo.Services.Dtos.CargoDetailDtos;

public class CreateCargoDetailDto
{
    public required int CargoCustomerId { get; set; }
    public required int CargoCompanyId { get; set; }
    public required string TrackingNumber { get; set; }
    public string? Description { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public DateTime DeliveredAt { get; set; }
    public bool IsDelivered { get; set; }
}
