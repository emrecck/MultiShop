namespace MultiShop.Web.Settings
{
    public class ClientSettings
    {
        public Client MultiShopVisitorClient { get; set; } = new();
        public Client MultiShopManagerClient { get; set; } = new();
        public Client MultiShopAdminClient { get; set; } = new();
    }

    public class Client
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}
