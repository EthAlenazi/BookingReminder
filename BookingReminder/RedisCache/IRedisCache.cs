namespace BookingReminder.RedisCache
{
    public interface IRedisCache
    {
        Task<bool> SetDataAsync(string key, string value, TimeSpan time);
        Task<string> GetDataAsync(string key);
        Task<bool> RemoveDataAsync(string key);
    }
}
