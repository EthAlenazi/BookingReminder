namespace BookingReminder.Settings
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
        public string EndPoint { get; set; }
        public int Port { get; set; }
        public bool AbortOnConnectFail { get; set; }
        public bool Enable { get; set; }
        public int AsyncTimeout { get; set; }
    }
}
