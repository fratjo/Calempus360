using System.Text.Json.Serialization;
using Calempus360.API.Handlers;
using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.Interfaces.AcademicYear;
using Calempus360.Core.Interfaces.Classroom;
using Calempus360.Core.Interfaces.Equipment;
using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Interfaces.University;
using Calempus360.Core.Interfaces.Group;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Repositories;
using Calempus360.Services.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Calempus360.Core.Interfaces.Option;
using Calempus360.Core.Interfaces.Course;
using Calempus360.Core.Interfaces.Session;
using Calempus360.Core.Interfaces.DayWithoutCourse;

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
builder.Services.AddScoped<IStudentGroupService, StudentGroupService>();
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IOptionService, OptionService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IDayWithoutCourseService, DayWithoutCourseService>();

// repositories
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
builder.Services.AddScoped<ISiteRepository, SitesRepository>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IStudentGroupRepository, StudentGroupRepository>();
builder.Services.AddScoped<IOptionRepository, OptionRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IDayWithoutCourseRepository, DayWithoutCourseRepository>();

// handlers
builder.Services.AddExceptionHandler<SessionConstraintExceptionHandler>();
builder.Services.AddExceptionHandler<ItemNotAvailableExceptionHandler>();
builder.Services.AddExceptionHandler<ExistingEntityExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<TestExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// validators
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UniversityRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SiteRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AcademicYearRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClassroomRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EquipmentRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<StudentGroupRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OptionRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CourseRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SessionRequestDtoValidator>();

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