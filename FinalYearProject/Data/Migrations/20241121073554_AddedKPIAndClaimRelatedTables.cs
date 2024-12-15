using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalYearProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedKPIAndClaimRelatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyKPIs",
                columns: table => new
                {
                    KPI_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    target_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    company_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    target_KPI = table.Column<float>(type: "real", nullable: true),
                    CurrentProgress = table.Column<float>(type: "real", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    hit_KPI_allowance = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyKPIs", x => x.KPI_id);
                    table.ForeignKey(
                        name: "FK_CompanyKPIs_Company_company_id",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeClaim",
                columns: table => new
                {
                    claim_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    staff_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    approval_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claim_reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_apply = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reject_reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claimAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claimFile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeClaim", x => x.claim_id);
                    table.ForeignKey(
                        name: "FK_EmployeeClaim_EmployeeDetails_staff_id",
                        column: x => x.staff_id,
                        principalTable: "EmployeeDetails",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeKPIs",
                columns: table => new
                {
                    EmployeeKPIId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KPI_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentProgress = table.Column<float>(type: "real", nullable: false),
                    ProgressStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeKPIs", x => x.EmployeeKPIId);
                    table.ForeignKey(
                        name: "FK_EmployeeKPIs_CompanyKPIs_KPI_id",
                        column: x => x.KPI_id,
                        principalTable: "CompanyKPIs",
                        principalColumn: "KPI_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KPIRewards",
                columns: table => new
                {
                    RewardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KPIId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HitKPIReward = table.Column<float>(type: "real", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIRewards", x => x.RewardId);
                    table.ForeignKey(
                        name: "FK_KPIRewards_CompanyKPIs_KPIId",
                        column: x => x.KPIId,
                        principalTable: "CompanyKPIs",
                        principalColumn: "KPI_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KPIRewards_EmployeeDetails_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetails",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProofSubmissions",
                columns: table => new
                {
                    ProofSubmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KPI_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KPIAmount = table.Column<float>(type: "real", nullable: false),
                    ProofFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProofSubmissions", x => x.ProofSubmissionId);
                    table.ForeignKey(
                        name: "FK_ProofSubmissions_CompanyKPIs_KPI_id",
                        column: x => x.KPI_id,
                        principalTable: "CompanyKPIs",
                        principalColumn: "KPI_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProofSubmissions_EmployeeDetails_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "EmployeeDetails",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalSalary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClaimAmount = table.Column<float>(type: "real", nullable: false),
                    ApprovedAmount = table.Column<float>(type: "real", nullable: false),
                    ClaimFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalSalary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalSalary_EmployeeClaim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "EmployeeClaim",
                        principalColumn: "claim_id");
                    table.ForeignKey(
                        name: "FK_AdditionalSalary_EmployeeDetails_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetails",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalSalary_ClaimId",
                table: "AdditionalSalary",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalSalary_EmployeeId",
                table: "AdditionalSalary",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyKPIs_company_id",
                table: "CompanyKPIs",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeClaim_staff_id",
                table: "EmployeeClaim",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeKPIs_KPI_id",
                table: "EmployeeKPIs",
                column: "KPI_id");

            migrationBuilder.CreateIndex(
                name: "IX_KPIRewards_EmployeeId",
                table: "KPIRewards",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIRewards_KPIId",
                table: "KPIRewards",
                column: "KPIId");

            migrationBuilder.CreateIndex(
                name: "IX_ProofSubmissions_Employee_id",
                table: "ProofSubmissions",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProofSubmissions_KPI_id",
                table: "ProofSubmissions",
                column: "KPI_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalSalary");

            migrationBuilder.DropTable(
                name: "EmployeeKPIs");

            migrationBuilder.DropTable(
                name: "KPIRewards");

            migrationBuilder.DropTable(
                name: "ProofSubmissions");

            migrationBuilder.DropTable(
                name: "EmployeeClaim");

            migrationBuilder.DropTable(
                name: "CompanyKPIs");
        }
    }
}
