using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.Interfaces;
using MMAC.Middleware;
using MMAC.Profiles;
using MMAC.Repositories;
using MMAC.Repositories.DashboardRepository;
using MMAC.Services;
using MMAC.Services.ArrivalInterface;
using MMAC.Services.AuditLogService;
using MMAC.Services.DashboardService;
using MMAC.Services.PdfService;
using MMAC.Services.PortOfArrivalService;
using MMAC.Services.SearchService;
using MMAC.Services.UpdateService;
using MMAC.Services.UtilityService;
using MMAC.Validations;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ── Database 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Hangfire 
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(c =>
        c.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));
builder.Services.AddHangfireServer();

// ── Controllers 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// ── Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── DI Services 
builder.Services.AddScoped<IPortOfArrivalRepository, PortOfArrivalRepository>();
builder.Services.AddScoped<IPortOfArrivalService, PortOfArrivalService>();
builder.Services.AddScoped<IForeignerSearchService, ForeignerSearchService>();
builder.Services.AddScoped<IMyanmarSearchService, MyanmarSearchService>();
builder.Services.AddScoped<ICompleteArrivalRepository, CompleteArrivalRepository>();
builder.Services.AddScoped<ICompleteArrivalService, CompleteArrivalService>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IUtilityService, UtilityService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddHttpContextAccessor();

// ── AutoMapper 
var mapperConfig = new MapperConfiguration(cfg =>
    cfg.AddProfile<CompleteArrivalMapper>());
builder.Services.AddSingleton(mapperConfig.CreateMapper());

// ── FluentValidation 
builder.Services.AddValidatorsFromAssemblyContaining<CompleteArrivalDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();

// ── CORS 
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// ── Middleware Pipeline 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors("AllowAll");
//app.UseWhen(
//    context => !context.Request.Path.StartsWithSegments("/hangfire"),
//    appBuilder => appBuilder.UseMiddleware<ApiKeyMiddleware>()
//);

app.UseAuthorization();

app.MapControllers();

app.MapHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new Hangfire.Dashboard.LocalRequestsOnlyAuthorizationFilter() }
});

app.Run();
