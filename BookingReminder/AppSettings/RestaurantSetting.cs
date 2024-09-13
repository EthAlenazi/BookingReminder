namespace BackendProject.AppSettings
{
    public class RestaurantSetting
    {
        public int RestaurantID { get; set; }
        public int BookingReminderMinutes { get; set; }

    }
    public interface IRestaurantSetting
    {
        int RestaurantID { get; }
        int BookingReminderMinutes { get; }
    }
}
