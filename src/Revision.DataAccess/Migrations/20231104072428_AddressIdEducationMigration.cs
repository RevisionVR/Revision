using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revision.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddressIdEducationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Addresses_AddressId",
                table: "Educations");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "Educations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Addresses_AddressId",
                table: "Educations",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Addresses_AddressId",
                table: "Educations");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "Educations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Addresses_AddressId",
                table: "Educations",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
