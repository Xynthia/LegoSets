﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegoSets.Migrations
{
    /// <inheritdoc />
    public partial class AddedOwnedLegoSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Owned",
                table: "LegoSet",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owned",
                table: "LegoSet");
        }
    }
}
