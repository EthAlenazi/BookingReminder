using BackendProject.DependancyInjection;
using BookingReminder.AppSettings;
using BookingReminder.RedisCache;
using NLog;
using NLog.Web;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Application starting...");

try
{
    var builder = WebApplication.CreateBuilder(args);
    //builder.Services.AddSingleton<Logger>(LogManager.GetCurrentClassLogger());
    // Add services to the container.
    builder.Services.ReadConfigurationsFiles(builder.Configuration);
builder.Services.AddCustomServicesInjecation(builder.Configuration);
    //builder.Services.AddSingleton<Logger>(LogManager.GetCurrentClassLogger());
    builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure NLog for ASP.NET Core
builder.Logging.ClearProviders();
    ILoggingBuilder loggingBuilder = builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


await ConnectionHelper.Init(app.Services.GetRequiredService<IRedisConfig>());
app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application stopped due to an exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
