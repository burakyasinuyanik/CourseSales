using CourseSales.Bus;
using CourseSales.Order.Api.EndPoints.Orders;
using CourseSales.Order.Application.Contracts.Refit;
using CourseSales.Order.Application.Contracts.Refit.PaymentService;
using CourseSales.Order.Application.Contracts.Repositories;
using CourseSales.Order.Application.Contracts.UnitOfWork;
using CourseSales.Order.Persistence;
using CourseSales.Order.Persistence.Repositories;
using CourseSales.Order.Persistence.UnitOfWork;
using CourseSales.Shared.Extensions;
using CourseSales.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddVersioningExt();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerOrder"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddCommonMasstransitExt(builder.Configuration);
builder.Services.AddRefitConfigurationExt(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddOrderGroupEndPointExt(app.AddVersionSetExt());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.Run();
