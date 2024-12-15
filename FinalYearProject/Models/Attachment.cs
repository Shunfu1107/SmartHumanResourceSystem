using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition;

namespace FinalYearProject.Models
{
    public class Attachment
    {
        [Key]
        public string? attachment_id { get; set; }

        [Required]
        public string? report_id { get; set; }

        [ForeignKey("report_id")]
        public virtual Report? Report { get; set; }

        [Required]
        public string? file_name { get; set; }

        [Required]
        public string? file_path { get; set; }

        public DateTime upload_date { get; set; }
    }
}
