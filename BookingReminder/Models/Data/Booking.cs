namespace BackendProject.Models.Data
{
    public class Booking
    {
        public int ID { get; set; }
        public DateTime BookingDate { get; set; }
        public int RestaurantID { get; set; }
        public Restaurant Restaurant { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }

}
