﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MadPay24.Data.Migrations
{
    public partial class changeBankEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "BankCards",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "BankCards");
        }
    }
}
