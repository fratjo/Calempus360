using System.Text.Json.Serialization;
using Calempus360.API.Handlers;
using Calempus360.Core.Interfaces.AcademicYear;
using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Interfaces.University;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Repositories;
using Calempus360.Services.AcademicYearService;
using Calempus360.Services.SiteService;
using Calempus360.Services.UniversityService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Calempus360DbContext>(options =>
{
    options
       .UseSqlServer(connectionString)
       .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
       .EnableSensitiveDataLogging(); // log sensitive data // remove in production ! 
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        ReferenceHandler.IgnoreCycles;
});

// DI Configuration
// services
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<IAcademicYearService, AcademciYearService>();
builder.Services.AddScoped<ISiteService, SiteService>();
// repositories
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
builder.Services.AddScoped<ISiteRepository, SitesRepository>();
// handlers
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<TestExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddFilter("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogLevel.None);
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
options.WithOrigins("http://localhost:4200")
       .AllowAnyMethod()
       .AllowAnyHeader());

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();