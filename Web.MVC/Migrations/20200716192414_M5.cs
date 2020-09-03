using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.MVC.Migrations
{
    public partial class M5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Tc",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "Tc",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
