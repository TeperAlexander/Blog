using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityCheck.Migrations
{
    public partial class PostCreatedAtAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9488abb0-d2d0-455d-840b-0291d9b1acdb", "3a6b2690-576c-4d95-887e-ed9d52d04e87" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9e45332b-1923-4aa0-a7bb-c992b55bcd24", "e90fd3c1-8e94-4cf2-a607-78c58628ce29" });

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9979def5-a7f3-4673-8e9b-1844053ff71f", "222bd0ee-634f-4777-b14d-1a003c408f4e", "SuperUser", "SUPERUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb315df2-4283-43a0-b512-be559e60fd25", "41fea183-ef1c-4314-ac86-d937c29487a8", "RegularUser", "REGULARUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9979def5-a7f3-4673-8e9b-1844053ff71f", "222bd0ee-634f-4777-b14d-1a003c408f4e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "bb315df2-4283-43a0-b512-be559e60fd25", "41fea183-ef1c-4314-ac86-d937c29487a8" });

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e45332b-1923-4aa0-a7bb-c992b55bcd24", "e90fd3c1-8e94-4cf2-a607-78c58628ce29", "SuperUser", "SUPERUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9488abb0-d2d0-455d-840b-0291d9b1acdb", "3a6b2690-576c-4d95-887e-ed9d52d04e87", "RegularUser", "REGULARUSER" });
        }
    }
}
