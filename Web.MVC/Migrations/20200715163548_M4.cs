using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.MVC.Migrations
{
    public partial class M4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Tc",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Tc",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
