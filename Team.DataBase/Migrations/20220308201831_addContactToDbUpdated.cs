using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team.DataBase.Migrations
{
    public partial class addContactToDbUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Contact",
                newName: "ContactNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Contact",
                newName: "PhoneNumber");
        }
    }
}
