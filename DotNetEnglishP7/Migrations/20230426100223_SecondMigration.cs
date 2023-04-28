using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetEnglishP7.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurvePointId",
                table: "CurvePoints",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurvePointId",
                table: "CurvePoints");
        }
    }
}
