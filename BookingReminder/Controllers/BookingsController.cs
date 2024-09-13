using BackendProject.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ReminderDelegate _reminderDelegate;

        public BookingsController(ReminderDelegate reminderDelegate)
        {
            _reminderDelegate = reminderDelegate;
        }

        [HttpGet("reminder-emails")]
        public async Task<IActionResult> GetReminderEmails()
        {
            DateTime currentTime= DateTime.Now;
            var emails = await _reminderDelegate.Invoke(currentTime);

            if (emails == null || emails.Count == 0)
            {
                return NotFound("No users with upcoming bookings.");
            }

            return Ok(emails);
        }
    }

}


