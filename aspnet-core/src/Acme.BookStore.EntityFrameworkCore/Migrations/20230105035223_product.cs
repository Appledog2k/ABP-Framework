using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    public partial class product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "AppProducts",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "PublicDate",
                table: "AppProducts",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "AppProducts",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "AppProducts",
                newName: "PublicDate");
        }
    }
}
