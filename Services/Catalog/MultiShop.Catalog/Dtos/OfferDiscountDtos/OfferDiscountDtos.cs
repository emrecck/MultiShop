namespace MultiShop.Catalog.Dtos.OfferDiscountDtos;

public class ResultOfferDiscountDto
{
    public string OfferDiscountId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ButtonTitle { get; set; } = string.Empty;
}

public class GetByIdOfferDiscountDto
{
    public string OfferDiscountId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ButtonTitle { get; set; } = string.Empty;
}

public class CreateOfferDiscountDto
{
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ButtonTitle { get; set; } = string.Empty;
}

public class UpdateOfferDiscountDto
{
    public string OfferDiscountId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ButtonTitle { get; set; } = string.Empty;
}
