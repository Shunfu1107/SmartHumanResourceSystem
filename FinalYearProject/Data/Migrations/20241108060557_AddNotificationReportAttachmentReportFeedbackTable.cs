using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalYearProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationReportAttachmentReportFeedbackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    notificationid = table.Column<string>(name: "notification_id", type: "nvarchar(450)", nullable: false),
                    notificationreceiver = table.Column<string>(name: "notification_receiver", type: "nvarchar(max)", nullable: false),
                    notificationtitle = table.Column<string>(name: "notification_title", type: "nvarchar(max)", nullable: false),
                    notificationdate = table.Column<DateTime>(name: "notification_date", type: "datetime2", nullable: false),
                    notificationcontent = table.Column<string>(name: "notification_content", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.notificationid);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    reportid = table.Column<string>(name: "report_id", type: "nvarchar(450)", nullable: false),
                    reportsender = table.Column<string>(name: "report_sender", type: "nvarchar(450)", nullable: false),
                    reportreceiver = table.Column<string>(name: "report_receiver", type: "nvarchar(max)", nullable: false),
                    reporttitle = table.Column<string>(name: "report_title", type: "nvarchar(max)", nullable: false),
                    reportdate = table.Column<DateTime>(name: "report_date", type: "datetime2", nullable: false),
                    reportcontent = table.Column<string>(name: "report_content", type: "nvarchar(max)", nullable: true),
                    reportstatus = table.Column<string>(name: "report_status", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.reportid);
                    table.ForeignKey(
                        name: "FK_Report_EmployeeDetails_report_sender",
                        column: x => x.reportsender,
                        principalTable: "EmployeeDetails",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    attachmentid = table.Column<string>(name: "attachment_id", type: "nvarchar(450)", nullable: false),
                    reportid = table.Column<string>(name: "report_id", type: "nvarchar(450)", nullable: false),
                    filename = table.Column<string>(name: "file_name", type: "nvarchar(max)", nullable: false),
                    filepath = table.Column<string>(name: "file_path", type: "nvarchar(max)", nullable: false),
                    uploaddate = table.Column<DateTime>(name: "upload_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.attachmentid);
                    table.ForeignKey(
                        name: "FK_Attachment_Report_report_id",
                        column: x => x.reportid,
                        principalTable: "Report",
                        principalColumn: "report_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportFeedback",
                columns: table => new
                {
                    feedbackid = table.Column<string>(name: "feedback_id", type: "nvarchar(450)", nullable: false),
                    reportid = table.Column<string>(name: "report_id", type: "nvarchar(450)", nullable: false),
                    feedbackcontent = table.Column<string>(name: "feedback_content", type: "nvarchar(max)", nullable: false),
                    feedbackdate = table.Column<DateTime>(name: "feedback_date", type: "datetime2", nullable: false),
                    employeeid = table.Column<string>(name: "employee_id", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFeedback", x => x.feedbackid);
                    table.ForeignKey(
                        name: "FK_ReportFeedback_Report_report_id",
                        column: x => x.reportid,
                        principalTable: "Report",
                        principalColumn: "report_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_report_id",
                table: "Attachment",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_Report_report_sender",
                table: "Report",
                column: "report_sender");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFeedback_report_id",
                table: "ReportFeedback",
                column: "report_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ReportFeedback");

            migrationBuilder.DropTable(
                name: "Report");
        }
    }
}
