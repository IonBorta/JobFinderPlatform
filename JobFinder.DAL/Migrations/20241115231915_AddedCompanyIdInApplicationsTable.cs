using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedCompanyIdInApplicationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Applications");
        }
    }
}
