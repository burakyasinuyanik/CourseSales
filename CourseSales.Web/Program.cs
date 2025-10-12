using CourseSales.Web.DelegateHandlers;
using CourseSales.Web.Extensions;
using CourseSales.Web.Options;
using CourseSales.Web.Pages.Auth.SignIn;
using CourseSales.Web.Pages.Auth.SignUp;
using CourseSales.Web.Services;
using CourseSales.Web.Services.Refit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;

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

builder.Services.AddRefitClient<ICatalogRefitService>()
    .ConfigureHttpClient(c =>
    {
        var gatewayOption = builder.Configuration.GetSection(nameof(GatewayOption)).Get<GatewayOption>();
        c.BaseAddress = new Uri(gatewayOption!.BaseAddress);
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
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
