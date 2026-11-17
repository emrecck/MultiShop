namespace MultiShop.Order.Domain.Entities;

public class Ordering
{
    public int Id { get; init; }
    public int CustomerId { get; init; }
    public decimal TotalPrice { get; set; }
    public DateTimeOffset OrderDate { get; init; }
    public List<OrderDetail> OrderDetails { get; init; }
}