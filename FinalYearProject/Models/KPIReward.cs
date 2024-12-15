using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class KPIReward
    {
        [Key]
        public int RewardId { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual EmployeeDetails Employee { get; set; }

        [Required]
        public string KPIId { get; set; }

        [ForeignKey("KPIId")]
        public virtual CompanyKPI CompanyKPI { get; set; }

        [Required]
        public float HitKPIReward { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}
