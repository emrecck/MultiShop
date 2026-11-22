using Microsoft.AspNetCore.Http.HttpResults;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoDetailDtos;

namespace MultiShop.Cargo.WebApi.Endpoints;

public static class CargoDetailEndpoints
{
    public static IEndpointRouteBuilder MapCargoDetailEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cargo-details")
            .WithTags("CargoDetails");

        group.MapGet("/", async (ICargoDetailService service, CancellationToken cancellationToken) =>
            TypedResults.Ok(await service.GetAllAsync(cancellationToken)))
            .WithName("GetAllCargoDetails")
            .WithSummary("Get all cargo details");

        group.MapGet("/{id:int}", async Task<Results<Ok<GetByIdCargoDetailDto>, NotFound>> (
            int id,
            ICargoDetailService service) =>
        {
            var detail = await service.GetByIdAsync(id);
            return detail is null ? TypedResults.NotFound() : TypedResults.Ok(detail);
        })
        .WithName("GetCargoDetailById")
        .WithSummary("Get a cargo detail by ID");

        group.MapPost("/", async (CreateCargoDetailDto dto, ICargoDetailService service) =>
        {
            var id = await service.CreateAsync(dto);
            return TypedResults.CreatedAtRoute(id, "GetCargoDetailById", new { id });
        })
        .WithName("CreateCargoDetail")
        .WithSummary("Create a cargo detail");

        group.MapPut("/{id:int}", async Task<Results<NoContent, BadRequest<string>, NotFound>> (
            int id,
            UpdateCargoDetailDto dto,
            ICargoDetailService service) =>
        {
            if (id != dto.Id)
                return TypedResults.BadRequest("Route ID must match request body ID.");

            var updated = await service.UpdateAsync(dto);
            return updated ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("UpdateCargoDetail")
        .WithSummary("Update a cargo detail");

        group.MapDelete("/{id:int}", async (int id, ICargoDetailService service) =>
        {
            await service.DeleteAsync(id);
            return TypedResults.NoContent();
        })
        .WithName("DeleteCargoDetail")
        .WithSummary("Delete a cargo detail");

        return app;
    }
}
