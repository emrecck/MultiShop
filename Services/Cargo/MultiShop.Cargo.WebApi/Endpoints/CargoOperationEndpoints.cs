using Microsoft.AspNetCore.Http.HttpResults;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoOperationDtos;

namespace MultiShop.Cargo.WebApi.Endpoints;

public static class CargoOperationEndpoints
{
    public static IEndpointRouteBuilder MapCargoOperationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cargo-operations")
            .WithTags("CargoOperations")
            .RequireAuthorization();

        group.MapGet("/", async (ICargoOperationService service, CancellationToken cancellationToken) =>
            TypedResults.Ok(await service.GetAllAsync(cancellationToken)))
            .WithName("GetAllCargoOperations")
            .WithSummary("Get all cargo operations");

        group.MapGet("/{id:int}", async Task<Results<Ok<GetByIdCargoOperationDto>, NotFound>> (
            int id,
            ICargoOperationService service) =>
        {
            var operation = await service.GetByIdAsync(id);
            return operation is null ? TypedResults.NotFound() : TypedResults.Ok(operation);
        })
        .WithName("GetCargoOperationById")
        .WithSummary("Get a cargo operation by ID");

        group.MapPost("/", async (CreateCargoOperationDto dto, ICargoOperationService service) =>
        {
            var id = await service.CreateAsync(dto);
            return TypedResults.CreatedAtRoute(id, "GetCargoOperationById", new { id });
        })
        .WithName("CreateCargoOperation")
        .WithSummary("Create a cargo operation");

        group.MapPut("/{id:int}", async Task<Results<NoContent, BadRequest<string>, NotFound>> (
            int id,
            UpdateCargoOperationDto dto,
            ICargoOperationService service) =>
        {
            if (id != dto.Id)
                return TypedResults.BadRequest("Route ID must match request body ID.");

            var updated = await service.UpdateAsync(dto);
            return updated ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("UpdateCargoOperation")
        .WithSummary("Update a cargo operation");

        group.MapDelete("/{id:int}", async (int id, ICargoOperationService service) =>
        {
            await service.DeleteAsync(id);
            return TypedResults.NoContent();
        })
        .WithName("DeleteCargoOperation")
        .WithSummary("Delete a cargo operation");

        return app;
    }
}
