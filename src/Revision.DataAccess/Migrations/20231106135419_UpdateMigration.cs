using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revision.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "DevicePayments");

            migrationBuilder.RenameColumn(
                name: "DeviceCount",
                table: "DevicePayments",
                newName: "Count");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "DevicePayments",
                newName: "DeviceCount");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "DevicePayments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
