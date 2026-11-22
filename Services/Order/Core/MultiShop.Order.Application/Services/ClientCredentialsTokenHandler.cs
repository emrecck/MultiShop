using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace MultiShop.Order.Application.Services;

public class ClientCredentialsTokenHandler : DelegatingHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;

    public ClientCredentialsTokenHandler(IHttpClientFactory httpClientFactory, IMemoryCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await GetAccessTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        const string cacheKey = "m2m_order_service_token";

        if (_cache.TryGetValue(cacheKey, out string? cachedToken) && !string.IsNullOrEmpty(cachedToken))
            return cachedToken;

        var client = _httpClientFactory.CreateClient();
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5166/connect/token")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = "order-service-client",
                ["client_secret"] = "order-super-secret-key-2026",
                ["scope"] = "DiscountReadPermission CatalogReadPermission"
            })
        };

        var response = await client.SendAsync(tokenRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadAsStringAsync(cancellationToken);
        var tokenResult = JsonSerializer.Deserialize<TokenResponse>(payload)
            ?? throw new InvalidOperationException("Token yanıtı çözümlenemedi.");

        // Token ömrü dolmadan 30 saniye önce cache'ten düşür
        var expiration = TimeSpan.FromSeconds(Math.Max(tokenResult.ExpiresIn - 30, 10));
        _cache.Set(cacheKey, tokenResult.AccessToken, expiration);

        return tokenResult.AccessToken;
    }

    private sealed record TokenResponse(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("expires_in")] int ExpiresIn);
}