using BackendProject.BackgroundJobs;

public class BackgroundReminderService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Logger<BackgroundReminderService> _logger;

    public BackgroundReminderService(IServiceProvider serviceProvider, Logger<BackgroundReminderService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var reminderDelegate = scope.ServiceProvider.GetRequiredService<ReminderDelegate>();

                    var emails = await reminderDelegate(DateTime.Now);
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
        catch (Exception ex) {
            _logger.LogError(ex.ToString());
        }
    }
}



