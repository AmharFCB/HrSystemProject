using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstProject.Migrations
{
    /// <inheritdoc />
    public partial class addEmployeeStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeStatusId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "employeeStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeStatusId",
                table: "Employees",
                column: "EmployeeStatusId");

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

            migrationBuilder.DropTable(
                name: "employeeStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeStatusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeStatusId",
                table: "Employees");
        }
    }
}
