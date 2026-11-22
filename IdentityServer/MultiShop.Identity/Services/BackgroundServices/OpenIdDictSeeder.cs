using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MultiShop.Identity.Services.BackgroundServices;

public class OpenIdDictSeeder(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();
        var appManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        // 1. SCOPE VE RESOURCE TANIMLARI
        await CreateScopeIfNotExistsAsync(scopeManager, "CatalogFullPermission", "Catalog tam yetki", "ResourceCatalog", cancellationToken);
        await CreateScopeIfNotExistsAsync(scopeManager, "CatalogReadPermission", "Catalog okuma yetkisi", "ResourceCatalog", cancellationToken);
        await CreateScopeIfNotExistsAsync(scopeManager, "OrderFullPermission", "Order tam yetki", "ResourceOrder", cancellationToken);
        await CreateScopeIfNotExistsAsync(scopeManager, "DiscountFullPermission", "Discount tam yetki", "ResourceDiscount", cancellationToken);
        await CreateScopeIfNotExistsAsync(scopeManager, "DiscountReadPermission", "Discount okuma yetkisi", "ResourceDiscount", cancellationToken);
        await CreateScopeIfNotExistsAsync(scopeManager, "CargoFullPermission", "Cargo tam yetki", "ResourceCargo", cancellationToken);
        await CreateScopeIfNotExistsAsync(scopeManager, "BasketFullPermission", "Basket tam yetki", "ResourceBasket", cancellationToken);

        // 2. MİKROSERVİS CLIENT TANIMLARI (Client Credentials)

        if (await appManager.FindByClientIdAsync("order-service-client", cancellationToken) is null)
        {
            await appManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "order-service-client",
                ClientSecret = "order-super-secret-key-2026",
                ClientType = ClientTypes.Confidential,
                DisplayName = "Order Mikroservice M2M Client",
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials,
                    Permissions.Prefixes.Scope + "CatalogReadPermission",
                    Permissions.Prefixes.Scope + "DiscountReadPermission"
                }
            }, cancellationToken);
        }

        // Catalog Mikroservisi (İleride başka servisleri çağırması gerekirse)
        if (await appManager.FindByClientIdAsync("catalog-service-client", cancellationToken) is null)
        {
            await appManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "catalog-service-client",
                ClientSecret = "catalog-super-secret-key-2026",
                ClientType = ClientTypes.Confidential,
                DisplayName = "Catalog Microservice M2M Client",
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials,
                    Permissions.Prefixes.Scope + "DiscountReadPermission"
                }
            }, cancellationToken);
        }

        // Discount Mikroservisi
        if (await appManager.FindByClientIdAsync("discount-service-client", cancellationToken) is null)
        {
            await appManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "discount-service-client",
                ClientSecret = "discount-super-secret-key-2026",
                ClientType = ClientTypes.Confidential,
                DisplayName = "Discount Microservice M2M Client",
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials
                }
            }, cancellationToken);
        }

        // Cargo Mikroservisi
        if (await appManager.FindByClientIdAsync("cargo-service-client", cancellationToken) is null)
        {
            await appManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "cargo-service-client",
                ClientSecret = "cargo-super-secret-key-2026",
                ClientType = ClientTypes.Confidential,
                DisplayName = "Cargo Microservice M2M Client",
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials,
                    Permissions.Prefixes.Scope + "CargoFullPermission"
                }
            }, cancellationToken);
        }

        // Basket Mikroservisi
        if (await appManager.FindByClientIdAsync("basket-service-client", cancellationToken) is null)
        {
            await appManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "basket-service-client",
                ClientType = ClientTypes.Public,
                DisplayName = "Basket Microservice M2M Client",
                RedirectUris = { new Uri("https://oauth.pstmn.io/v1/callback") },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Email,
                    Permissions.Prefixes.Scope + "BasketFullPermission"
                },
                Requirements = {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            }, cancellationToken);
        }
    }

    private static async Task CreateScopeIfNotExistsAsync(
        IOpenIddictScopeManager manager,
        string name,
        string displayName,
        string resource,
        CancellationToken ct)
    {
        if (await manager.FindByNameAsync(name, ct) is null)
        {
            await manager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = name,
                DisplayName = displayName,
                Resources = { resource } // JWT içindeki 'aud' (Audience) alanına bu değer yazılır
            }, ct);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}