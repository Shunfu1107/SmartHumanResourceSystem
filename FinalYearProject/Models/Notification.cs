using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    public class Notification
    {
        [Display(Name = "Notification ID")]
        [Key]
        public string? notification_id { get; set; }

        [Required]
        [Display(Name = "Notification Receiver")]
        public string? notification_receiver { get; set; }

        [Required]
        [Display(Name = "Notification Title")]
        public string? notification_title { get; set; }

        [Required]
        [Display(Name = "Notification Date")]
        public DateTime notification_date { get; set; }

        [Required]
        [Display(Name = "Notification Content")]
        public string? notification_content { get; set; }

        [Display(Name = "Notification Type")]
        public string? notification_type { get; set; }
    }
}
