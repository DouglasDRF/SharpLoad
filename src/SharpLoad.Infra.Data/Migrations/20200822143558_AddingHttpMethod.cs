using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpLoad.Infra.Data.Migrations
{
    public partial class AddingHttpMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "Request",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Method",
                table: "Request");
        }
    }
}
