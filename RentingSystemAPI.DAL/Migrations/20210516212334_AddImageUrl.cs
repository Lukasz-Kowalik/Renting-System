using Microsoft.EntityFrameworkCore.Migrations;

namespace RentingSystemAPI.DAL.Migrations
{
    public partial class AddImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentationURL",
                table: "Items",
                newName: "DocumentationUrl");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Items",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "DocumentationUrl",
                table: "Items",
                newName: "DocumentationURL");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "16d622da-359b-432f-8253-29b7a54786f3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b358e8a4-6fcd-4863-b58a-54e300f5fbfe");
        }
    }
}
