using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class ProofSubmission
    {
        [Key]
        public int ProofSubmissionId { get; set; }

        [Required]
        public string Employee_id { get; set; }
        [ForeignKey("Employee_id")]
        public virtual EmployeeDetails Employee { get; set; }

        [Required]
        public string KPI_id { get; set; }
        [ForeignKey("KPI_id")]
        public virtual CompanyKPI? CompanyKPI { get; set; }

        public float KPIAmount { get; set; }

        public string? ProofFileUrl { get; set; }

        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";

        public string? AdminComment { get; set; }
    }
}
