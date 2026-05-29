using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.Profiles;
using MMAC.Repositories;
using MMAC.Services;
using MMAC.Services.ArrivalInterface;
using MMAC.Services.UtilityService;
using MMAC.Validations;
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
builder.Services.AddScoped<IPurposeOfVisitService, PurposeOfVisitService>();
builder.Services.AddScoped<IPurposeOfVisitRepository, PurposeOfVisitRepository>();

builder.Services.AddScoped<IPortOfArrivalRepository, PortOfArrivalRepository>();
builder.Services.AddScoped<IPortOfArrivalService, PortOfArrivalService>();


builder.Services.AddScoped<ICompleteArrival, CompleteArrivalService>();

builder.Services.AddScoped<ICompleteArrivalRepository, CompleteArrivalRepository>();

builder.Services.AddScoped<IUtilityService, UtilityService>();



// AutoMapper Core Version 13 Configuration
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<CompleteArrivalMapper>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddValidatorsFromAssemblyContaining<CompleteArrivalDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseCors("AllowAll");
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
