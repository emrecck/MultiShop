namespace MultiShop.Cargo.Services.Dtos.CargoDetailDtos;

public class GetByIdCargoDetailDto
{
    public int Id { get; set; }
    public int CargoCustomerId { get; set; }
    public int CargoCompanyId { get; set; }
    public required string TrackingNumber { get; set; }
    public string? Description { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public DateTime DeliveredAt { get; set; }
    public bool IsDelivered { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
