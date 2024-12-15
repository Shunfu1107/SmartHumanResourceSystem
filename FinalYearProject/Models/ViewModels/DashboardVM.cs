using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Models.ViewModels
{
    public class DashboardVM
    {
        public List<Notification> TodayNotificationsList { get; set; }
        public List<Leave> TodayLeavesList { get; set; }
        public List<Leave> UpcomingLeaves { get; set; }
        public int TotalEmployees { get; set; }
        public int TodayLeaves { get; set; }
        public int TodayNotifications { get; set; }
        public DateTime TodayDate { get; set; }
    }
}
