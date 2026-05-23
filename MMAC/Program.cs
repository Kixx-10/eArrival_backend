using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Database Injection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Indection for service
builder.Services.AddScoped<IPortOfArrivalService, PortOfArrivalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
