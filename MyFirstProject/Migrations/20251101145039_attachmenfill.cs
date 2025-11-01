using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstProject.Migrations
{
    /// <inheritdoc />
    public partial class attachmenfill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentPath",
                table: "Attendances");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPath",
                table: "LeaveRequestss",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentPath",
                table: "LeaveRequestss");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPath",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
