namespace MultiShop.Web.Settings
{
    public class ServiceApiSettings
    {
        public string OcelotUrl { get; set; } = string.Empty;
        public string IdentityServerUrl { get; set; } = string.Empty;
        public ServiceApi Catalog { get; set; } = new();
        public ServiceApi Image { get; set; } = new();
        public ServiceApi Discount { get; set; } = new();
        public ServiceApi Order { get; set; } = new();
        public ServiceApi Cargo { get; set; } = new();
        public ServiceApi Basket { get; set; } = new();
        public ServiceApi Payment { get; set; } = new();
        public ServiceApi Comment { get; set; } = new();
        public ServiceApi Message { get; set; } = new();
    }

    public class ServiceApi
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public string EffectiveBaseAddress(string fallbackOcelotUrl) =>
            !string.IsNullOrWhiteSpace(BaseUrl)
                ? BaseUrl.TrimEnd('/') + "/"
                : $"{fallbackOcelotUrl.TrimEnd('/')}/{Path.TrimStart('/')}";
    }
}
