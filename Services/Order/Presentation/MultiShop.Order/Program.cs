using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Application.Features.Addresses;
using MultiShop.Order.Application.Features.OrderDetails;
using MultiShop.Order.Application.Features.Orders;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Application.Services;
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Token'ı üreten Identity projenizin adresi
        options.Authority = "https://localhost:7046";

        // Bu mikroservisin Resource adı (Seeder'daki Resources ile birebir eşleşmeli)
        options.Audience = "ResourceOrder";

        // Local ortamda HTTPS sertifika uyarısı almamak için
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    // OpenIddict birden fazla scope'u tek bir string içinde boşlukla ayırarak döner:
    // "scope": "CatalogReadPermission DiscountReadPermission"
    // Bu nedenle Split ile kontrol etmek en güvenli yöntemdir.
    options.AddPolicy("OrderReadPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context =>
            context.User.FindAll("scope")
                .SelectMany(c => c.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Contains("OrderReadPermission"));
    });
});

builder.Services.AddMemoryCache();
builder.Services.AddTransient<ClientCredentialsTokenHandler>();

// Discount servisine gidecek tüm istekler otomatik Bearer token taşır
builder.Services.AddHttpClient("DiscountApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5128"); // Discount API adresi
})
.AddHttpMessageHandler<ClientCredentialsTokenHandler>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();