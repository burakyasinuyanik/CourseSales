using CourseSales.Basket.Api;
using CourseSales.Basket.Api.Features.Baskets;
using CourseSales.Shared.Extensions;
using CourseSales.Bus;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddCommonMasstransitExt(builder.Configuration);

builder.Services.AddVersioningExt();
builder.Services.AddScoped<BasketService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});




var app = builder.Build();


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
