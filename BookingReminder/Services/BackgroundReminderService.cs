using BackendProject.Helpers;

public class BackgroundReminderService : IHostedService, IDisposable
{
    private readonly ReminderDelegate _reminderDelegate;
    private Timer _timer;

    public BackgroundReminderService(ReminderDelegate reminderDelegate)
    {
        _reminderDelegate = reminderDelegate;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Set the timer to execute every 60 seconds
        _timer = new Timer(RunReminderLogic, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
        return Task.CompletedTask;
    }

    private void RunReminderLogic(object state)
    {
        DateTime currentTime = DateTime.Now;
        var emails = _reminderDelegate.Invoke(currentTime);

       
        Console.WriteLine("Emails to notify:");
        foreach (var email in emails)
        {
            Console.WriteLine(email);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}


