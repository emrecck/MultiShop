using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Coupons.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<MultiShop.Discount.Coupons.DapperContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICouponService, CouponService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Token'ı üreten Identity projenizin adresi
        options.Authority = "https://localhost:7046";

        // Bu mikroservisin Resource adı (Seeder'daki Resources ile birebir eşleşmeli)
        options.Audience = "ResourceDiscount";

        // Local ortamda HTTPS sertifika uyarısı almamak için
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    // OpenIddict birden fazla scope'u tek bir string içinde boşlukla ayırarak döner:
    // "scope": "CatalogReadPermission DiscountReadPermission"
    // Bu nedenle Split ile kontrol etmek en güvenli yöntemdir.
    options.AddPolicy("DiscountReadPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context =>
            context.User.FindAll("scope")
                .SelectMany(c => c.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Contains("DiscountReadPermission"));
    });
});

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

app.UseAuthentication(); // UseAuthorization'dan ÖNCE gelmeli

app.UseAuthorization();

app.MapControllers();

app.Run();
