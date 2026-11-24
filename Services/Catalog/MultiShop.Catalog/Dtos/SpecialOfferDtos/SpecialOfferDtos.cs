namespace MultiShop.Catalog.Dtos.SpecialOfferDtos;

public class ResultSpecialOfferDto
{
    public string SpecialOfferId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}

public class GetByIdSpecialOfferDto
{
    public string SpecialOfferId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}

public class CreateSpecialOfferDto
{
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}

public class UpdateSpecialOfferDto
{
    public string SpecialOfferId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
