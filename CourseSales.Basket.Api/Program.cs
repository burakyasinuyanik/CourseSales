using CourseSales.Basket.Api;
using CourseSales.Basket.Api.Features.Baskets;
using CourseSales.Shared.Extensions;
using CourseSales.Bus;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddMasstransitExt(builder.Configuration);

builder.Services.AddVersioningExt();
builder.Services.AddScoped<BasketService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});




var app = builder.Build();

app.MapDefaultEndpoints();

app.UseExceptionHandler(x => { });
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.AddBasketGroupEndPointExt(app.AddVersionSetExt());


app.Run();
