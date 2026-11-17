using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Application.Features.Addresses;
using MultiShop.Order.Application.Features.OrderDetails;
using MultiShop.Order.Application.Features.Orders;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Middleware;
using MultiShop.Order.Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("OrderDatabase")
    ?? throw new InvalidOperationException("Connection string 'OrderDatabase' is not configured.");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddApplicationPart(typeof(AdressesController).Assembly);
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<AdressesController>());
builder.Services.AddScoped<AddressMapper>();
builder.Services.AddScoped<OrderDetailMapper>();
builder.Services.AddScoped<OrderMapper>();
builder.Services.AddDbContext<OrderContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddExceptionHandler<EntityNotFoundExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();