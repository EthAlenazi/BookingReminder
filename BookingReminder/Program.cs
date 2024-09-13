using BackendProject.DependancyInjection;
using BookingReminder.RedisCache;
using BookingReminder.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ReadConfigurationsFiles(builder.Configuration);
builder.Services.AddCustomServicesInjecation(builder.Configuration);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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


ConnectionHelper.Init(app.Services.GetRequiredService<IRedisConfig>());
app.Run();
