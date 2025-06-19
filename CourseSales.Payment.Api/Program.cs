using CourseSales.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddVersioningExt();
builder.Services.AddSwaggerGen();

var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
   
}

app.UseHttpsRedirection();




app.Run();


