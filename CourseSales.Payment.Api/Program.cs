using CourseSales.Payment.Api.Feature.Payment;
using CourseSales.Payment.Api.Repositories;
using CourseSales.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddVersioningExt();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("payment-in-memory-db");
});
var app = builder.Build();
app.AddPaymentGroupEntPointExt(app.AddVersionSetExt());



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
   
}

app.UseHttpsRedirection();




app.Run();


