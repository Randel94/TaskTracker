using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Migrations
{
    public partial class TaskDecimalTypeCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedTime",
                table: "Task",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CompletedTime",
                table: "Task",
                type: "decimal(6,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedTime",
                table: "Task",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CompletedTime",
                table: "Task",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldNullable: true);
        }
    }
}
