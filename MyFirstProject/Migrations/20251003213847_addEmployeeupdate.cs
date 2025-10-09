using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstProject.Migrations
{
    /// <inheritdoc />
    public partial class addEmployeeupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_employeeStatuses_EmployeeStatus",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeStatus",
                table: "Employees",
                newName: "EmployeeStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeStatus",
                table: "Employees",
                newName: "IX_Employees_EmployeeStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_employeeStatuses_EmployeeStatusId",
                table: "Employees",
                column: "EmployeeStatusId",
                principalTable: "employeeStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_employeeStatuses_EmployeeStatusId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeStatusId",
                table: "Employees",
                newName: "EmployeeStatus");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeStatusId",
                table: "Employees",
                newName: "IX_Employees_EmployeeStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_employeeStatuses_EmployeeStatus",
                table: "Employees",
                column: "EmployeeStatus",
                principalTable: "employeeStatuses",
                principalColumn: "Id");
        }
    }
}
