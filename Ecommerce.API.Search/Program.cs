using Ecommerce.API.Search.Interfaces;
using Ecommerce.API.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddHttpClient("OrderService", config =>
{
    config.BaseAddress = new Uri(configuration["Services:Orders"]);
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services.AddHttpClient("ProductService", config =>
{
    config.BaseAddress = new Uri(configuration["Services:Products"]);
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services.AddHttpClient("CustomersService", config =>
{
    config.BaseAddress = new Uri(configuration["Services:Customers"]);
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
