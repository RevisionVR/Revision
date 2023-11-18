using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revision.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DevicePaymentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "DevicePayments");

            migrationBuilder.RenameColumn(
                name: "NextDay",
                table: "TopicPayments",
                newName: "NextDate");

            migrationBuilder.RenameColumn(
                name: "LastDay",
                table: "TopicPayments",
                newName: "LastDate");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "DevicePayments",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "NextDay",
                table: "DevicePayments",
                newName: "NextDate");

            migrationBuilder.RenameColumn(
                name: "LastDay",
                table: "DevicePayments",
                newName: "LastDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NextDate",
                table: "TopicPayments",
                newName: "NextDay");

            migrationBuilder.RenameColumn(
                name: "LastDate",
                table: "TopicPayments",
                newName: "LastDay");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "DevicePayments",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "NextDate",
                table: "DevicePayments",
                newName: "NextDay");

            migrationBuilder.RenameColumn(
                name: "LastDate",
                table: "DevicePayments",
                newName: "LastDay");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "DevicePayments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
