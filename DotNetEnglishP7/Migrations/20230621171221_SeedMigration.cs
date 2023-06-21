using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetEnglishP7.Migrations
{
    /// <inheritdoc />
    public partial class SeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dbc35e5c-c409-4ebc-86f6-17f90ec05a22");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ebdb44d4-d740-43f1-8699-20b56de34527");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "93a14a8e-a6c1-4a86-9750-54b6523ba6e2", null, false, "Super Admin", false, null, null, "SUPERADMIN", "AQAAAAIAAYagAAAAEL2apa3wUSu3nIhQjlVHOR7MqQ+a9hMct2WTNMnm3cu4qzEV1Vo7xYTx9ejZfhvvPw==", null, false, "c78d2dfe-f372-4e31-b0dd-3d424667acbd", false, "superAdmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 2, 0, "ca21b12a-df17-4218-a813-48a2356bf112", null, false, "Standard User", false, null, null, "STANDARDUSER", "AQAAAAIAAYagAAAAECeqFhDKD9Exv2qGJDdCHFpkEFjmxSQZcxAfbtPSB/zs9kLGwrHuKBPE0hluiElX8Q==", null, false, "5422f7e9-6fbb-43e4-92b5-0e3580813941", false, "standardUser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c74ff728-0e02-40c6-9013-8bbf9e289273");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5c59b33a-51fd-4099-83b7-9f04de206c4a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "FullName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "269e433c-98e6-4442-b840-28f693f4814b", "superadmin", "AQAAAAIAAYagAAAAEOOGNt8J8qhY5kglp1lgSMWA0UgV4LeGJppdX6+xBrBBVjoqc4dSPgd46+XztlhVTA==", "9abfcc41-acf2-4d13-8ff7-f750703c3276" });
        }
    }
}
