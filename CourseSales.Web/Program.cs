using CourseSales.Web.DelegateHandlers;
using CourseSales.Web.ExceptionHandlers;
using CourseSales.Web.Extensions;
using CourseSales.Web.Options;
using CourseSales.Web.Pages.Auth.SignIn;
using CourseSales.Web.Pages.Auth.SignUp;
using CourseSales.Web.Services;
using CourseSales.Web.Services.Refit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOptionsExt();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<SignUpService>(options =>
{


});
builder.Services.AddHttpClient<SignInService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<AuthenticatedHttpClientHandler>();
builder.Services.AddScoped<ClientAuthenticatedHttpClientHandler>();
builder.Services.AddScoped<UserService>();

builder.Services.AddRefitClient<ICatalogRefitService>()
    .ConfigureHttpClient(c =>
    {
        var microServiceOption = builder.Configuration.GetSection(nameof(MicroServiceOption)).Get<MicroServiceOption>();
        c.BaseAddress = new Uri(microServiceOption!.Catalog.BaseUrl);
    })
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
    .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.Cookie.Name = "CourseSales";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.LoginPath = "/Auth/SignIn";
        //options.LogoutPath = "/Auth/SignOut";
    });
builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();


builder.Services.AddAuthorization();
var app = builder.Build();
var cultureInfo= new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo),
    SupportedCultures =  [cultureInfo ],
    SupportedUICultures = [cultureInfo]
});
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
   
    app.UseHsts();
}
app.UseExceptionHandler("/Error");
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
