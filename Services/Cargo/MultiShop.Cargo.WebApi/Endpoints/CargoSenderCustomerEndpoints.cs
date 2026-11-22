using Microsoft.AspNetCore.Http.HttpResults;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoSenderCustomerDtos;

namespace MultiShop.Cargo.WebApi.Endpoints;

public static class CargoSenderCustomerEndpoints
{
    public static IEndpointRouteBuilder MapCargoSenderCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cargo-sender-customers")
            .WithTags("CargoSenderCustomers");

        group.MapGet("/", async (ICargoSenderCustomerService service, CancellationToken cancellationToken) =>
            TypedResults.Ok(await service.GetAllAsync(cancellationToken)))
            .WithName("GetAllCargoSenderCustomers")
            .WithSummary("Get all cargo sender customers");

        group.MapGet("/{id:int}", async Task<Results<Ok<GetByIdCargoSenderCustomerDto>, NotFound>> (
            int id,
            ICargoSenderCustomerService service) =>
        {
            var customer = await service.GetByIdAsync(id);
            return customer is null ? TypedResults.NotFound() : TypedResults.Ok(customer);
        })
        .WithName("GetCargoSenderCustomerById")
        .WithSummary("Get a cargo sender customer by ID");

        group.MapPost("/", async (CreateCargoSenderCustomerDto dto, ICargoSenderCustomerService service) =>
        {
            var id = await service.CreateAsync(dto);
            return TypedResults.CreatedAtRoute(id, "GetCargoSenderCustomerById", new { id });
        })
        .WithName("CreateCargoSenderCustomer")
        .WithSummary("Create a cargo sender customer");

        group.MapPut("/{id:int}", async Task<Results<NoContent, BadRequest<string>, NotFound>> (
            int id,
            UpdateCargoSenderCustomerDto dto,
            ICargoSenderCustomerService service) =>
        {
            if (id != dto.Id)
                return TypedResults.BadRequest("Route ID must match request body ID.");

            var updated = await service.UpdateAsync(dto);
            return updated ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("UpdateCargoSenderCustomer")
        .WithSummary("Update a cargo sender customer");

        group.MapDelete("/{id:int}", async (int id, ICargoSenderCustomerService service) =>
        {
            await service.DeleteAsync(id);
            return TypedResults.NoContent();
        })
        .WithName("DeleteCargoSenderCustomer")
        .WithSummary("Delete a cargo sender customer");

        return app;
    }
}
