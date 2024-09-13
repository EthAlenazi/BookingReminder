namespace BookingReminder.AppSettings
{
    public class RedisConfig : IRedisConfig
    {
        public string EndPoint { get; set; }
        public int Port { get; set; }
        public bool AbortOnConnectFail { get; set; }
        public bool Enable { get; set; }
        public int AsyncTimeout { get; set; }
    }
    public interface IRedisConfig
    {
        string EndPoint { get; }
        int Port { get; }
        bool AbortOnConnectFail { get; }
        bool Enable { get; }
        int AsyncTimeout { get; }
    }
}
