using CourseSales.Payment.Api.Feature.Payment;
using CourseSales.Payment.Api.Repositories;
using CourseSales.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using CourseSales.Bus;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddVersioningExt();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("payment-in-memory-db");
});
builder.Services.AddCommonMasstransitExt(builder.Configuration);


var app = builder.Build();

app.MapDefaultEndpoints();
app.UseExceptionHandler(x => { });
app.AddPaymentGroupEntPointExt(app.AddVersionSetExt());



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
   
}


app.UseAuthentication();
app.UseAuthorization();



app.Run();


