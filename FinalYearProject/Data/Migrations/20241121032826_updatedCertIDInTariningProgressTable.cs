using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalYearProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedCertIDInTariningProgressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgress_Document_cert_id",
                table: "TrainingProgress");

            migrationBuilder.AlterColumn<string>(
                name: "cert_id",
                table: "TrainingProgress",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgress_Document_cert_id",
                table: "TrainingProgress",
                column: "cert_id",
                principalTable: "Document",
                principalColumn: "document_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgress_Document_cert_id",
                table: "TrainingProgress");

            migrationBuilder.AlterColumn<string>(
                name: "cert_id",
                table: "TrainingProgress",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgress_Document_cert_id",
                table: "TrainingProgress",
                column: "cert_id",
                principalTable: "Document",
                principalColumn: "document_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
