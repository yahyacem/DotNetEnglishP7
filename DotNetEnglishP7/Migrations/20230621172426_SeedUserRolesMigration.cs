using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotNetEnglishP7.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRolesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "da061d11-5867-4000-b560-7e84a0e8e976");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1bdec776-f7b7-4cc7-8c7a-9529538fc0ac");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d72fece0-81c4-4b83-a951-d4d168be8722", "AQAAAAIAAYagAAAAEDR/KdVWT0R3/jDUBHJrVJLgXJeZhv9gQpal5gEsHBSX3EZhNl20Z+DBI9DvIeuk7Q==", "c4a194d8-9681-40ae-9d8f-cff6a139530a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e660363-5d4e-46d1-a4b4-746d8b5d483f", "AQAAAAIAAYagAAAAEKARAqTCDdbdnfVpdf+jTM4k9IaLF07VqRW/TrWeC8NVIe0/Ul2Z3ZioWMZdymEXPA==", "33e9b626-c533-4595-9d5b-a540eeeda156" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93a14a8e-a6c1-4a86-9750-54b6523ba6e2", "AQAAAAIAAYagAAAAEL2apa3wUSu3nIhQjlVHOR7MqQ+a9hMct2WTNMnm3cu4qzEV1Vo7xYTx9ejZfhvvPw==", "c78d2dfe-f372-4e31-b0dd-3d424667acbd" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca21b12a-df17-4218-a813-48a2356bf112", "AQAAAAIAAYagAAAAECeqFhDKD9Exv2qGJDdCHFpkEFjmxSQZcxAfbtPSB/zs9kLGwrHuKBPE0hluiElX8Q==", "5422f7e9-6fbb-43e4-92b5-0e3580813941" });
        }
    }
}
