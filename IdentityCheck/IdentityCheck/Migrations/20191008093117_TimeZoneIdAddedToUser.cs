using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityCheck.Migrations
{
    public partial class TimeZoneIdAddedToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "61aaf8ee-143d-4e32-8de0-48fbb63ccad1", "67035405-642b-477d-a2b0-658372777026" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8e50aa6b-08eb-45b5-8304-347526bac819", "b23a923a-7697-4d35-8ddf-e97023c73e88" });

            migrationBuilder.AddColumn<string>(
                name: "TimeZoneId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e45332b-1923-4aa0-a7bb-c992b55bcd24", "e90fd3c1-8e94-4cf2-a607-78c58628ce29", "SuperUser", "SUPERUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9488abb0-d2d0-455d-840b-0291d9b1acdb", "3a6b2690-576c-4d95-887e-ed9d52d04e87", "RegularUser", "REGULARUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9488abb0-d2d0-455d-840b-0291d9b1acdb", "3a6b2690-576c-4d95-887e-ed9d52d04e87" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9e45332b-1923-4aa0-a7bb-c992b55bcd24", "e90fd3c1-8e94-4cf2-a607-78c58628ce29" });

            migrationBuilder.DropColumn(
                name: "TimeZoneId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "61aaf8ee-143d-4e32-8de0-48fbb63ccad1", "67035405-642b-477d-a2b0-658372777026", "SuperUser", "SUPERUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8e50aa6b-08eb-45b5-8304-347526bac819", "b23a923a-7697-4d35-8ddf-e97023c73e88", "RegularUser", "REGULARUSER" });
        }
    }
}
