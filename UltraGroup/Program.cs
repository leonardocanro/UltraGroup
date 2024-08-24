using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UltraGroup;
using Prometheus;
using Serilog;
using Infrastructure.Data.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

// las dos siguientes lineas son para realizar pruebas de integración, se debe habilitar un BD en Memoria
//builder.Services.AddDbContext<ContextoPrincipal>(options =>
//    options.UseInMemoryDatabase("TestDatabase"));


// Configuración de Serilog
builder.Services.AddApplicationInsightsTelemetry();
Log.Logger = new LoggerConfiguration()
              .WriteTo.Console()
              .MinimumLevel.Information()
              .Enrich.FromLogContext()
              .CreateLogger();
builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
               .WriteTo.ApplicationInsights(
                       services.GetRequiredService<TelemetryConfiguration>(),
                       TelemetryConverter.Traces));

Log.Information("Starting the application...");
// Database connection
//var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
//var dbName = Environment.GetEnvironmentVariable("DB_NAME");
//var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
//var connectionstring = $"Data Source={dbHost};Initial Catalog={dbName};User Id=sa;Password={dbPassword};TrustServerCertificate=true";
var connectionstring = $"Server=tcp:servidorpruebas01.database.windows.net,1433;Initial Catalog=AgenciaViajes;Persist Security Info=False;User ID=administradorbdpruebas;Password=Aa1234567!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
builder.Services.AddDbContext<ContextoPrincipal>(opt => opt.UseSqlServer(connectionstring));
// Reemplaza el logger por defecto con Serilog
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Host.UseSerilog(Log.Logger);

// Configura la autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// Add services to the container.

builder.Services.AddControllers();

// Configuración de Prometheus
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Configura los servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API Deploy NLCR", Version = "v1" });

    // Configura el esquema de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Por favor, introduzca el token JWT en el formato 'Bearer {token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

//Register the API configuration
ConfigureServiceExtension.InitConfigurationAPI(builder.Services, builder.Configuration);

// Register the automapper config
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        Log.Error(exception, "Unhandled exception occurred. {ExceptionDetails}", exception?.ToString());

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    });
});

// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}
if (!app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication(); // Debe ir antes de UseAuthorization
app.UseAuthorization();

// Endpoints de Prometheus
app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics(); // Exponer las métricas en /metrics
});

app.MapControllers();
Log.Information("Application started successfully.");
app.Run();

