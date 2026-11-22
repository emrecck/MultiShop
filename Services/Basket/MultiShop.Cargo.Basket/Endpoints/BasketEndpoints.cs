using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using MultiShop.Cargo.Basket.Models;

namespace MultiShop.Cargo.Basket.Endpoints;

public static class BasketEndpoints
{
    public static IEndpointRouteBuilder MapBasketEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/basket")
            .WithTags("Basket")
            .RequireAuthorization("BasketFullPolicy");

        group.MapGet("/", async Task<Results<Ok<BasketTotalDto>, NotFound, UnauthorizedHttpResult>> (
            ClaimsPrincipal user,
            IRedisService redisService) =>
        {
            var userId = GetUserId(user);
            if (string.IsNullOrWhiteSpace(userId))
                return TypedResults.Unauthorized();

            var basket = await redisService.GetValueAsync<BasketTotalDto>(GetBasketKey(userId));
            return basket is null ? TypedResults.NotFound() : TypedResults.Ok(basket);
        })
        .WithName("GetBasket")
        .WithSummary("Get the authenticated user's basket");

        group.MapPost("/", async Task<Results<Ok<BasketTotalDto>, UnauthorizedHttpResult, ProblemHttpResult>> (
            BasketTotalDto basket,
            ClaimsPrincipal user,
            IRedisService redisService) =>
        {
            var userId = GetUserId(user);
            if (string.IsNullOrWhiteSpace(userId))
                return TypedResults.Unauthorized();

            basket.UserId = userId;

            var saved = await redisService.SetValueAsync(
                GetBasketKey(userId),
                JsonSerializer.Serialize(basket));

            return saved
                ? TypedResults.Ok(basket)
                : TypedResults.Problem("Basket could not be saved to Redis.");
        })
        .WithName("SaveBasket")
        .WithSummary("Save or update the authenticated user's basket");

        group.MapDelete("/", async Task<Results<NoContent, UnauthorizedHttpResult>> (
            ClaimsPrincipal user,
            IRedisService redisService) =>
        {
            var userId = GetUserId(user);
            if (string.IsNullOrWhiteSpace(userId))
                return TypedResults.Unauthorized();

            await redisService.Clear(GetBasketKey(userId));
            return TypedResults.NoContent();
        })
        .WithName("DeleteBasket")
        .WithSummary("Clear the authenticated user's basket");

        return endpoints;
    }

    private static string? GetUserId(ClaimsPrincipal user) =>
        user.FindFirstValue("sub") ?? user.FindFirstValue(ClaimTypes.NameIdentifier);

    private static string GetBasketKey(string userId) => $"basket:{userId}";
}
