using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityCheck.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6652217a-f203-4e29-bfd0-7a444818a632", "790c8206-4909-4553-a543-88b3728b54b9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "900f2c0b-2b79-48f7-a7e5-c4f41bd1843f", "b5bc1ca2-8b9a-4d4e-a760-b057fec1393b" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a0084da7-061f-4e59-92b8-348edaebf284", "8a076ada-a5f9-4070-93c5-d18b57bd4e6f", "SuperUser", "SUPERUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ab5cbaa-6522-4d1a-b21c-ceba38b0071c", "96f8a790-915a-40a2-9338-7b23054e9244", "RegularUser", "REGULARUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0ab5cbaa-6522-4d1a-b21c-ceba38b0071c", "96f8a790-915a-40a2-9338-7b23054e9244" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a0084da7-061f-4e59-92b8-348edaebf284", "8a076ada-a5f9-4070-93c5-d18b57bd4e6f" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6652217a-f203-4e29-bfd0-7a444818a632", "790c8206-4909-4553-a543-88b3728b54b9", "SuperUser", "SUPERUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "900f2c0b-2b79-48f7-a7e5-c4f41bd1843f", "b5bc1ca2-8b9a-4d4e-a760-b057fec1393b", "RegularUser", "REGULARUSER" });
        }
    }
}
