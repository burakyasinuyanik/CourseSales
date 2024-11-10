using CourseSales.Catalog.Api.Features.Categories;
using CourseSales.Catalog.Api.Options;
using CourseSales.Catalog.Api.Repositories;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();
builder.Services.AddDataBaseServiceExt();


var app = builder.Build();

app.AddCategoryGroupEndPointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.Run();
