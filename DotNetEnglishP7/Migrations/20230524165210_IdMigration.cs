﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetEnglishP7.Migrations
{
    /// <inheritdoc />
    public partial class IdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TradeId",
                table: "Trades",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BidListId",
                table: "BidLists",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Trades",
                newName: "TradeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BidLists",
                newName: "BidListId");
        }
    }
}
