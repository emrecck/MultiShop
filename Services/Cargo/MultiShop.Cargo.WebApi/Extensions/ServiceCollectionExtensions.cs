using MultiShop.Cargo.Repositories.EntityFramework;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;
using MultiShop.Cargo.Services.Abstract;
using MultiShop.Cargo.Services.Concrete;

namespace MultiShop.Cargo.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCargoServices(this IServiceCollection services)
    {
        //services.AddScoped<IEfCargoCompanyRepository, EFCargoCompanyRepository>();
        //services.AddScoped<IEfCargoDetailRepository, EFCargoDetailRepository>();
        //services.AddScoped<IEfCargoOperationRepository, EFCargoOperationRepository>();
        //services.AddScoped<IEfCargoReceiverCustomerRepository, EFCargoReceiverCustomerRepository>();
        //services.AddScoped<IEfCargoSenderCustomerRepository, EFCargoSenderCustomerRepository>();

        services.AddScoped<ICargoCompanyService, CargoCompanyManager>();
        services.AddScoped<ICargoDetailService, CargoDetailManager>();
        services.AddScoped<ICargoOperationService, CargoOperationManager>();
        services.AddScoped<ICargoReceiverCustomerService, CargoReceiverCustomerManager>();
        services.AddScoped<ICargoSenderCustomerService, CargoSenderCustomerManager>();

        return services;
    }
}
