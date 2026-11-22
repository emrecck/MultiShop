using Microsoft.AspNetCore.Http.HttpResults;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoReceiverCustomerDtos;

namespace MultiShop.Cargo.WebApi.Endpoints;

public static class CargoReceiverCustomerEndpoints
{
    public static IEndpointRouteBuilder MapCargoReceiverCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cargo-receiver-customers")
            .WithTags("CargoReceiverCustomers");

        group.MapGet("/", async (ICargoReceiverCustomerService service, CancellationToken cancellationToken) =>
            TypedResults.Ok(await service.GetAllAsync(cancellationToken)))
            .WithName("GetAllCargoReceiverCustomers")
            .WithSummary("Get all cargo receiver customers");

        group.MapGet("/{id:int}", async Task<Results<Ok<GetByIdCargoReceiverCustomerDto>, NotFound>> (
            int id,
            ICargoReceiverCustomerService service) =>
        {
            var customer = await service.GetByIdAsync(id);
            return customer is null ? TypedResults.NotFound() : TypedResults.Ok(customer);
        })
        .WithName("GetCargoReceiverCustomerById")
        .WithSummary("Get a cargo receiver customer by ID");

        group.MapPost("/", async (CreateCargoReceiverCustomerDto dto, ICargoReceiverCustomerService service) =>
        {
            var id = await service.CreateAsync(dto);
            return TypedResults.CreatedAtRoute(id, "GetCargoReceiverCustomerById", new { id });
        })
        .WithName("CreateCargoReceiverCustomer")
        .WithSummary("Create a cargo receiver customer");

        group.MapPut("/{id:int}", async Task<Results<NoContent, BadRequest<string>, NotFound>> (
            int id,
            UpdateCargoReceiverCustomerDto dto,
            ICargoReceiverCustomerService service) =>
        {
            if (id != dto.Id)
                return TypedResults.BadRequest("Route ID must match request body ID.");

            var updated = await service.UpdateAsync(dto);
            return updated ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("UpdateCargoReceiverCustomer")
        .WithSummary("Update a cargo receiver customer");

        group.MapDelete("/{id:int}", async (int id, ICargoReceiverCustomerService service) =>
        {
            await service.DeleteAsync(id);
            return TypedResults.NoContent();
        })
        .WithName("DeleteCargoReceiverCustomer")
        .WithSummary("Delete a cargo receiver customer");

        return app;
    }
}
