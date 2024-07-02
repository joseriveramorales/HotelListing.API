using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HoteListing.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e5341db-722d-4f07-b625-6e1cf83e3098");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31ea67b4-9f1e-44a4-88af-ceb031d1691f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "070c01ba-7384-4f93-97f9-e5d8620a27a6", null, "Administrator", "ADMINISTRATOR" },
                    { "2de02ce1-d172-4866-9fcf-a84eb07704c4", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "0e5341db-722d-4f07-b625-6e1cf83e3098", null, "User", "USER" },
                    { "31ea67b4-9f1e-44a4-88af-ceb031d1691f", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
