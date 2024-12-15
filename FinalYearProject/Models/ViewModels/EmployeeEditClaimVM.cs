using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models.ViewModels
{
    public class EmployeeEditClaimVM
    {
        public EmployeeClaim claim { get; set; }


        [Required(ErrorMessage = "Claim Amount is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid amount format.")]
        public string claimAmount { get; set; } // Keep as string for binding

        [Required(ErrorMessage = "Claim reason is required.")]
        [Display(Name = "Claim Reason")]
        public string claim_reason { get; set; }

        public string claimFile { get; set; }
    }
}
