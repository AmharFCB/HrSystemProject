using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstProject.Migrations
{
    /// <inheritdoc />
    public partial class addUid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "LeaveRequestss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uid",
                table: "LeaveRequestss");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Attendances");
        }
    }
}
