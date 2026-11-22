using MultiShop.Cargo.WebApi.Endpoints;

namespace MultiShop.Cargo.WebApi.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapCargoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCargoCompanyEndpoints();
        app.MapCargoDetailEndpoints();
        app.MapCargoOperationEndpoints();
        app.MapCargoReceiverCustomerEndpoints();
        app.MapCargoSenderCustomerEndpoints();

        return app;
    }
}
