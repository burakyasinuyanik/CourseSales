using CourseSales.Discount.Api;
using CourseSales.Discount.Api.Features.Discounts;
using CourseSales.Discount.Api.Options;
using CourseSales.Discount.Api.Repositories;
using CourseSales.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioningExt();
builder.Services.AddOptionExt();
builder.Services.AddDataBaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();

app.AddDiscountGroupEndPointExt(app.AddVersionSetExt());

app.AddSeedDataExt().ContinueWith(x =>
{
    if (!x.IsFaulted)
        Console.WriteLine("seed data yüklendi");
    else Console.WriteLine(x.Exception.Message);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.Run();

