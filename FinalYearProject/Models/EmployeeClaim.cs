using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    public class EmployeeClaim
    {
        [Key]
        [Display(Name = "Claim ID")]
        public string? claim_id { get; set; }

        [Required]
        [Display(Name = "Staff ID")]
        public string? staff_id { get; set; }

        [ForeignKey("staff_id")]
        public virtual EmployeeDetails? EmployeeDetails { get; set; }

        [Display(Name = "Status")]
        public string? approval_status { get; set; } = "Pending";

        [Display(Name = "Claim Reason")]
        public string? claim_reason { get; set; }

        [Required]
        [Display(Name = "Apply Date")]
        public DateTime date_apply { get; set; }

        [Display(Name = "Reject Reason (Optional)")]
        public string? reject_reason { get; set; }

        [Required(ErrorMessage = "The Claim Amount is required.")]
        [Display(Name = "Claim Amount (RM)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "The Claim Amount must be a valid number.")]
        public string? claimAmount { get; set; }

        [Display(Name = "Claim Document")]
        [FileExtensions(Extensions = "jpg,jpeg,png,pdf,doc,docx", ErrorMessage = "Please upload a valid file type (jpg, jpeg, png, pdf, doc, docx).")]
        public string? claimFile { get; set; }
    }

}
