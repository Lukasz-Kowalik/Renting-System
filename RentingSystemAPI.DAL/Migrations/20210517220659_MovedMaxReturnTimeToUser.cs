using Microsoft.EntityFrameworkCore.Migrations;

namespace RentingSystemAPI.DAL.Migrations
{
    public partial class MovedMaxReturnTimeToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxReturnTimeInDays",
                table: "Rents");

            migrationBuilder.AddColumn<int>(
                name: "MaxReturnTimeInDays",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "812b013b-649c-4219-b32a-dbb5444a744b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0bbb9e32-834f-4e65-a61a-6f977727acf7");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxReturnTimeInDays",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "MaxReturnTimeInDays",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4a37babb-e59c-42ae-9e73-694c1497fe8b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "baf1ffd4-8eb4-4cf9-bf27-81751460f9d6");
        }
    }
}
