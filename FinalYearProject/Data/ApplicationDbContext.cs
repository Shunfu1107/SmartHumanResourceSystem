using FinalYearProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FinalYearProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TrainingProgress>().HasKey(table => new {
                table.staff_id,
                table.training_id
            });

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Payrate> Payrate { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<TrainingProgress> TrainingProgress { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Benefit> Benefit { get; set; }
        public DbSet<Compensation> Compensation { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<Payroll> Payroll { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<ReportFeedback> ReportFeedback { get; set; }
        public DbSet<EmployeeClaim> EmployeeClaim { get; set; }
        public DbSet<AdditionalSalary> AdditionalSalary { get; set; }
        public DbSet<EmployeeKPI> EmployeeKPIs { get; set; }
        public DbSet<ProofSubmission> ProofSubmissions { get; set; }
        public DbSet<CompanyKPI> CompanyKPIs { get; set; }
        public DbSet<KPIReward> KPIRewards { get; set; }
        public DbSet<IntervieweeProgress> IntervieweeProgress { get; set; }
    }
}