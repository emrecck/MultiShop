using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiShop.Identity.Models;
using MultiShop.Identity.Services.BackgroundServices;
using OpenIddict.Abstractions;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityContext") ?? throw new InvalidOperationException("Connection string 'IdentityContext' not found.");

builder.Services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseOpenIddict();
});

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddOpenIddict()
.AddCore(options =>
{
    options.UseEntityFrameworkCore()
           .UseDbContext<IdentityContext>();
})
.AddServer(options =>
{
    options.SetTokenEndpointUris("/connect/token")
    .SetAuthorizationEndpointUris("/connect/authorize")
    .SetUserInfoEndpointUris("/connect/userinfo")
    .SetEndSessionEndpointUris("/connect/logout");

    options.AllowClientCredentialsFlow()
           .AllowPasswordFlow()
           .AllowRefreshTokenFlow()
           .AllowAuthorizationCodeFlow()
           .RequireProofKeyForCodeExchange();

    options.RegisterScopes(
        OpenIddictConstants.Scopes.OpenId,
        OpenIddictConstants.Scopes.Profile,
        OpenIddictConstants.Scopes.Email,
        OpenIddictConstants.Scopes.OfflineAccess,
        "CatalogFullPermission",
        "CatalogReadPermission",
        "OrderFullPermission",
        "DiscountFullPermission",
        "DiscountReadPermission",
        "CargoFullPermission",
        "BasketFullPermission",
        "CommentFullPermission"
    );

    // Local geliştirme için imzalama ve şifreleme sertifikaları
    options.AddDevelopmentEncryptionCertificate()
           .AddDevelopmentSigningCertificate();


    options.DisableAccessTokenEncryption();


    options.UseAspNetCore()
        .EnableAuthorizationEndpointPassthrough()
        .EnableTokenEndpointPassthrough()
        .EnableUserInfoEndpointPassthrough()
        .EnableEndSessionEndpointPassthrough();
})
.AddValidation(options =>
{
    options.UseLocalServer();
    options.UseAspNetCore();
});

// Scope ve Client kayıtlarını veritabanına ekleyecek HostedService
builder.Services.AddHostedService<OpenIdDictSeeder>();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapControllers(); // Controller route'larını aktif eder

await app.RunAsync();
