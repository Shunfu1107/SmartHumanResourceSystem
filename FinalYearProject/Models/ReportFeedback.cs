using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class ReportFeedback
    {
        [Display(Name = "Feedback ID")]
        [Key]
        public string? feedback_id { get; set; }

        [Required]
        public string? report_id { get; set; }

        [ForeignKey("report_id")]
        public virtual Report? Report { get; set; }

        [Required]
        public string? feedback_content { get; set; }

        public DateTime feedback_date { get; set; }

        [Required]
        public string? employee_id { get; set; }

    }
}
