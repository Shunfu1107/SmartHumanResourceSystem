using FinalYearProject.Models;
using System.Collections.Generic;
using FinalYearProject.Models.ViewModels;


namespace FinalYearProject.Models.ViewModels
{
    public class AdditionalSalaryVM
    {
        public EmployeeDetails Employee { get; set; }
        public List<AdditionalSalary> AdditionalSalaries { get; set; }

        public List<KPIReward> KPIRewards { get; set; }

        public List<ProofSubmission> ProofSubmissions { get; set; }

    }
}
