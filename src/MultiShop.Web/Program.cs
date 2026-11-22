using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Web.Handlers;
using MultiShop.Web.Services.BasketServices;
using MultiShop.Web.Services.CargoServices.CargoCompanyServices;
using MultiShop.Web.Services.CargoServices.CargoCustomerServices;
using MultiShop.Web.Services.CatalogServices.AboutServices;
using MultiShop.Web.Services.CatalogServices.BrandServices;
using MultiShop.Web.Services.CatalogServices.CategoryServices;
using MultiShop.Web.Services.CatalogServices.ContactServices;
using MultiShop.Web.Services.CatalogServices.FeatureServices;
using MultiShop.Web.Services.CatalogServices.FeatureSliderServices;
using MultiShop.Web.Services.CatalogServices.OfferDiscountServices;
using MultiShop.Web.Services.CatalogServices.ProductDetailServices;
using MultiShop.Web.Services.CatalogServices.ProductImageServices;
using MultiShop.Web.Services.CatalogServices.ProductServices;
using MultiShop.Web.Services.CatalogServices.SpecialOfferServices;
using MultiShop.Web.Services.CommentServices;
using MultiShop.Web.Services.Concrete;
using MultiShop.Web.Services.DiscountServices;
using MultiShop.Web.Services.Interfaces;
using MultiShop.Web.Services.MessageServices;
using MultiShop.Web.Services.OrderServices.OrderAddressServices;
using MultiShop.Web.Services.OrderServices.OrderOderingServices;
using MultiShop.Web.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.Web.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.Web.Services.StatisticServices.MessageStatisticServices;
using MultiShop.Web.Services.StatisticServices.UserStatisticServices;
using MultiShop.Web.Services.UserIdentityServices;
using MultiShop.Web.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/";
        opt.ExpireTimeSpan = TimeSpan.FromDays(5);
        opt.Cookie.Name = "MultiShopCookie";
        opt.SlidingExpiration = true;
    })
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/";
        opt.LogoutPath = "/Login/LogOut/";
        opt.AccessDeniedPath = "/Pages/AccessDenied/";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "MultiShopJwt";
    });

builder.Services.AddAccessTokenManagement();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddHttpClient<IIdentityService, IdentityService>();

builder.Services.AddHttpClient();
builder.Services.AddRazorPages();

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

var values = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>() ?? new ServiceApiSettings();

builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICatalogStatisticService, CatalogStatisticService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IMessageStatisticService, MessageStatisticService>(opt =>
{
    opt.BaseAddress = new Uri(values.Message.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IDiscountStatisticService, DiscountStatisticService>(opt =>
{
    opt.BaseAddress = new Uri(values.Discount.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IUserStatisticService, UserStatisticService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri(values.Basket.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderOderingService, OrderOderingService>(opt =>
{
    opt.BaseAddress = new Uri(values.Order.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderAddressService, OrderAddressService>(opt =>
{
    opt.BaseAddress = new Uri(values.Order.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IDiscountService, DiscountService>(opt =>
{
    opt.BaseAddress = new Uri(values.Discount.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<ICargoCompanyService, CargoCompanyService>(opt =>
{
    opt.BaseAddress = new Uri(values.Cargo.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICargoCustomerService, CargoCustomerService>(opt =>
{
    opt.BaseAddress = new Uri(values.Cargo.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IMessageService, MessageService>(opt =>
{
    opt.BaseAddress = new Uri(values.Message.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICategoryService, CategoryService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductService, ProductService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<ISpecialOfferService, SpecialOfferService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IFeatureSliderService, FeatureSliderService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IFeatureService, FeatureService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IOfferDiscountService, OfferDiscountService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IBrandService, BrandService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IAboutService, AboutService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductImageService, ProductImageService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<ICommentService, CommentService>(opt =>
{
    opt.BaseAddress = new Uri(values.Comment.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IContactService, ContactService>(opt =>
{
    opt.BaseAddress = new Uri(values.Catalog.EffectiveBaseAddress(values.OcelotUrl));
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
