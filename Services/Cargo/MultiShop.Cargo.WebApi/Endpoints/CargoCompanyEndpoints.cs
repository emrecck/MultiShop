using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Dtos.CargoCompanyDtos;

namespace MultiShop.Cargo.WebApi.Endpoints;

public static class CargoCompanyEndpoints
{
    public static IEndpointRouteBuilder MapCargoCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cargo-companies")
            .WithTags("CargoCompanies")
            .RequireAuthorization();

        group.MapGet("/", async (ICargoCompanyService service, CancellationToken cancellationToken) =>
            TypedResults.Ok(await service.GetAllAsync(cancellationToken)))
            .WithName("GetAllCargoCompanies")
            .WithSummary("Get all cargo companies");

        group.MapGet("/{id:int}", async Task<Results<Ok<GetByIdCargoCompanyDto>, NotFound>> (
            int id,
            ICargoCompanyService service) =>
        {
            var company = await service.GetByIdAsync(id);
            return company is null ? TypedResults.NotFound() : TypedResults.Ok(company);
        })
        .WithName("GetCargoCompanyById")
        .WithSummary("Get a cargo company by ID");

        group.MapPost("/", async (CreateCargoCompanyDto dto, ICargoCompanyService service) =>
        {
            var id = await service.CreateAsync(dto);
            return TypedResults.CreatedAtRoute(id, "GetCargoCompanyById", new { id });
        })
        .WithName("CreateCargoCompany")
        .WithSummary("Create a cargo company");

        group.MapPut("/{id:int}", async Task<Results<NoContent, BadRequest<string>, NotFound>> (
            int id,
            UpdateCargoCompanyDto dto,
            ICargoCompanyService service) =>
        {
            if (id != dto.Id)
                return TypedResults.BadRequest("Route ID must match request body ID.");

            var updated = await service.UpdateAsync(dto);
            return updated ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("UpdateCargoCompany")
        .WithSummary("Update a cargo company");

        group.MapDelete("/{id:int}", async (int id, ICargoCompanyService service) =>
        {
            await service.DeleteAsync(id);
            return TypedResults.NoContent();
        })
        .WithName("DeleteCargoCompany")
        .WithSummary("Delete a cargo company");

        return app;
    }
}
