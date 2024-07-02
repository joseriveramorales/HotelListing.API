using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HoteListing.API.Migrations
{
    /// <inheritdoc />
    public partial class OneMoreTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "070c01ba-7384-4f93-97f9-e5d8620a27a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2de02ce1-d172-4866-9fcf-a84eb07704c4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "76e422d8-f628-4989-9187-5bbcf39665de", null, "User", "USER" },
                    { "ca33f84a-f434-4d0d-a2f8-7c64d1b90df7", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76e422d8-f628-4989-9187-5bbcf39665de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca33f84a-f434-4d0d-a2f8-7c64d1b90df7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "070c01ba-7384-4f93-97f9-e5d8620a27a6", null, "Administrator", "ADMINISTRATOR" },
                    { "2de02ce1-d172-4866-9fcf-a84eb07704c4", null, "User", "USER" }
                });
        }
    }
}
