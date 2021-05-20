using Microsoft.EntityFrameworkCore.Migrations;

namespace RentingSystemAPI.DAL.Migrations
{
    public partial class AddedNullableForItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d35fa037-3319-4644-9c25-ced6c5dfe25b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3accac14-ea8f-4782-855c-0d747e152a91");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
