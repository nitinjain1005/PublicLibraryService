using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using PublicLibraryService.API.Middleware;
using PublicLibraryService.API.Swagger;
using PublicLibraryService.Application.Filters;
using PublicLibraryService.Application.Mappings;
using PublicLibraryService.Application.Services;
using PublicLibraryService.Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.File("logs/log_.log", rollingInterval: RollingInterval.Day);
});

// Add AutoMapper

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MapperProfile>();
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    // Use whatever reader you want
    options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader());
}).AddApiExplorer(options =>
{

    options.GroupNameFormat = "'v'VVV";


    options.SubstituteApiVersionInUrl = true;
});


// Add Application Services
builder.Services.AddApplicationServices();



// MVC + FluentValidation
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.AddInfrastructureServices(builder.Configuration);

// Add Swagger configuration


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();


var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();


// Global Error Handler Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"Public Library API {description.GroupName.ToUpperInvariant()}"
            );
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
