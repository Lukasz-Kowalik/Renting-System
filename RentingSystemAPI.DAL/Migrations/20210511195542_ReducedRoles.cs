using Microsoft.EntityFrameworkCore.Migrations;

namespace RentingSystemAPI.DAL.Migrations
{
    public partial class ReducedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

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
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b358e8a4-6fcd-4863-b58a-54e300f5fbfe", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5dd8aad7-fc03-4654-a5ab-61bce90e348c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "de535f20-e0f8-4023-8aff-2d261ea90785", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 3, "52f31bc3-8658-442c-ad2a-8f99504a3923", "Worker", "WORKER" },
                    { 4, "defc341f-844f-41b4-8f2f-9ca8e702491f", "Admin", "ADMIN" }
                });
        }
    }
}
