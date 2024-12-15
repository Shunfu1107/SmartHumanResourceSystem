using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class AdditionalSalary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual EmployeeDetails Employee { get; set; }

        // Claim-related properties
        public string? ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public virtual EmployeeClaim? Claim { get; set; }

        [Required]
        public float ClaimAmount { get; set; }

        [Required]
        public float ApprovedAmount { get; set; }

        public string? ClaimFile { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}
