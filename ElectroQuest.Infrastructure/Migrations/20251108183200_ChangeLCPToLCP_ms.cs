using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectroQuest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLCPToLCP_ms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LCP",
                table: "RowData",
                newName: "LCP_ms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LCP_ms",
                table: "RowData",
                newName: "LCP");
        }
    }
}
