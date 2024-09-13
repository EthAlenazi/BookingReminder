namespace BookingReminder.RedisCache
{
    public interface IRedisCache
    {
        Task SetDataAsync<T>(string key, T data, TimeSpan time);
        Task<T> GetDataAsync<T>(string key) where T : new();
        Task RemoveDataAsync(string key);
    }
}
