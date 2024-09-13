using BackendProject.Helpers;

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
                
                var emails =  reminderDelegate(DateTime.Now);

  
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // 60 seconds delay for test 
         // await Task.Delay(TimeSpan.FromHours(1), stoppingToken); 
        }
    }
}



