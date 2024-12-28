using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Application.Shared.Migrations
{
    /// <inheritdoc />
    public partial class keyAutoGrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Users",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "RoleId");
        }
    }
}
