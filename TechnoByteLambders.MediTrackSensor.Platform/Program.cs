using Microsoft.EntityFrameworkCore;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.Internal.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.Internal.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Infrastructure.Persistence.EFC.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Application.Internal.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Application.Internal.OutboundServices;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Infrastructure.OutboundServices;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Application.Internal.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Infrastructure.Persistence.EFC.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Application.Internal.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Application.Internal.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Domain.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Logistics.Infrastructure.Persistence.EFC.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Application.Internal.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Application.Internal.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Domain.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Monitoring.Infrastructure.Persistence.EFC.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Domain.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Application.Internal.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Application.Internal.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Domain.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS — permite llamadas desde el frontend Vue (dev y prod)
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",   // Vite dev server
                "http://localhost:4173",   // Vite preview
                "https://meditrack-sensor.vercel.app" // Vercel prod (ajustar si cambia)
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Database
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM — Infrastructure
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
// IAM — Application
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IAdminCommandService, AdminCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IAdminQueryService, AdminQueryService>();
// IAM — Outbound
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Establishments — Infrastructure
builder.Services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
builder.Services.AddScoped<IOperatorRepository, OperatorRepository>();
// Establishments — Application
builder.Services.AddScoped<IEstablishmentCommandService, EstablishmentCommandService>();
builder.Services.AddScoped<IOperatorCommandService, OperatorCommandService>();
builder.Services.AddScoped<IEstablishmentQueryService, EstablishmentQueryService>();
builder.Services.AddScoped<IOperatorQueryService, OperatorQueryService>();

// Monitoring — Infrastructure
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
// Monitoring — Application
builder.Services.AddScoped<IDeviceCommandService, DeviceCommandService>();
builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();

// Logistics — Infrastructure
builder.Services.AddScoped<ITransportRepository, TransportRepository>();
// Logistics — Application
builder.Services.AddScoped<ITransportCommandService, TransportCommandService>();
builder.Services.AddScoped<ITransportQueryService, TransportQueryService>();

// Subscriptions — Infrastructure
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
// Subscriptions — Application
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("FrontendPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();
