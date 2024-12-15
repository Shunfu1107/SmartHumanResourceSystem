using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class Report
    {
        [Display(Name = "Report ID")]
        [Key]
        public string? report_id { get; set; }

        [Required]
        [Display(Name = "Report Sender")]
        public string? report_sender { get; set; }

        [ForeignKey("report_sender")]
        public virtual EmployeeDetails? SendByEmployee { get; set; }

        [Required]
        [Display(Name = "Report Receiver")]
        public string? report_receiver { get; set; }

        [Required]
        [Display(Name = "Report Title")]
        public string? report_title { get; set; }

        [Required]
        [Display(Name = "Report Date")]
        public DateTime report_date { get; set; }

        [Display(Name = "Report Content")]
        public string? report_content { get; set; }

        [Display(Name = "Report Status")]
        public string? report_status { get; set; }

        public virtual ICollection<Attachment>? Attachments { get; set; }
    }
}
