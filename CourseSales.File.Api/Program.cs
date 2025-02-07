using CourseSales.File.Api;
using CourseSales.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersioningExt();
builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddSingleton<IFileProvider>(Path.Combine(Directory.GetCurrentDirectory()))

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();





app.Run();


