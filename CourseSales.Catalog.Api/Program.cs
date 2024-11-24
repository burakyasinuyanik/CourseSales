using CourseSales.Catalog.Api;
using CourseSales.Catalog.Api.Features.Categories;
using CourseSales.Catalog.Api.Features.Courses;
using CourseSales.Catalog.Api.Options;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();
builder.Services.AddDataBaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddApiVersioning();


var app = builder.Build();

app.AddCategoryGroupEndPointExt(app.AddVersionSetExt());
app.AddCourseGroupEndPointExt(app.AddVersionSetExt());
app.AddSeedDataExt().ContinueWith(x =>
{
    if (x.IsFaulted)
    {
        Console.WriteLine(x.Exception.Message);
    }
    else
    {
        Console.WriteLine("seed data yüklendi.");
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.Run();
