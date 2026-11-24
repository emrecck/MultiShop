namespace MultiShop.Catalog.Dtos.FeatureDtos;

public class ResultFeatureDto
{
    public string FeatureId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public class GetByIdFeatureDto
{
    public string FeatureId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public class CreateFeatureDto
{
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public class UpdateFeatureDto
{
    public string FeatureId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}
