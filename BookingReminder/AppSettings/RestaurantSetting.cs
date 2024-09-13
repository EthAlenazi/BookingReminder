namespace BackendProject.Settings
{
    public class RestaurantSetting
    {
        public int RestaurantID { get; set; }
        public int BookingReminderMinutes { get; set; }

    }
    public interface IRestaurantSetting
    {
        public int RestaurantID { get; set; }
        public int BookingReminderMinutes { get; set; }

    }
}
