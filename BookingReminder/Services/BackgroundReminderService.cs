using BackendProject.BackgroundJobs;

public class BackgroundReminderService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public BackgroundReminderService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
}



