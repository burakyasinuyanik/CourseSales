using CourseSales.File.Api;
using CourseSales.File.Api.Features.Files;
using CourseSales.Shared.Extensions;
using Microsoft.Extensions.FileProviders;
using CourseSales.Bus;
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersioningExt();
builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddMasstransitExt(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseExceptionHandler(x => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.AddFileGroupEndPointExt(app.AddVersionSetExt());
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();



app.Run();


