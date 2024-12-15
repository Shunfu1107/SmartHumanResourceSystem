using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    public class IntervieweeProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? EmployeeId { get; set; }

        [Required]
        public string? TrainingId { get; set; }

        public string? ResultImagePath { get; set; }
        public bool IsApproved { get; set; } = false;

        public string? Status { get; set; } = "Pending";

        // Navigation properties
        public virtual Training? Training { get; set; }
    }
}
