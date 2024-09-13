namespace BackendProject.BackgroundJobs
{
    public delegate Task<List<string>> ReminderDelegate(DateTime currentTime);

}
