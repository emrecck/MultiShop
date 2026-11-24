namespace MultiShop.Catalog.Dtos.FeatureSliderDtos;

public class ResultFeatureSliderDto
{
    public string FeatureSliderId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool Status { get; set; }
}

public class GetByIdFeatureSliderDto
{
    public string FeatureSliderId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool Status { get; set; }
}

public class CreateFeatureSliderDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool Status { get; set; }
}

public class UpdateFeatureSliderDto
{
    public string FeatureSliderId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool Status { get; set; }
}
