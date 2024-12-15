using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalYearProject.Models
{
    public class EmployeeKPI
    {
        [Key]
        public int EmployeeKPIId { get; set; }

        public string Employee_id { get; set; }

        public string KPI_id { get; set; }

        [ForeignKey("KPI_id")]
        public virtual CompanyKPI CompanyKPI { get; set; }

        public float CurrentProgress { get; set; } = 0;

        public string ProgressStatus { get; set; } = "InProgress";
    }
}
