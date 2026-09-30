using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "11111111-1111-1111-1111-111111111111", "SystemAdmin", "SYSTEMADMIN" },
                    { "2", "22222222-2222-2222-2222-222222222222", "CompanyAdmin", "COMPANYADMIN" },
                    { "3", "33333333-3333-3333-3333-333333333333", "ProjectManager", "PROJECTMANAGER" },
                    { "4", "44444444-4444-4444-4444-444444444444", "SiteManager", "SITEMANAGER" },
                    { "5", "55555555-5555-5555-5555-555555555555", "Engineer", "ENGINEER" },
                    { "6", "66666666-6666-6666-6666-666666666666", "Accountant", "ACCOUNTANT" },
                    { "7", "77777777-7777-7777-7777-777777777777", "Procurement", "PROCUREMENT" },
                    { "8", "88888888-8888-8888-8888-888888888888", "Warehouse", "WAREHOUSE" },
                    { "9", "99999999-9999-9999-9999-999999999999", "Viewer", "VIEWER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9");
        }
    }
}
