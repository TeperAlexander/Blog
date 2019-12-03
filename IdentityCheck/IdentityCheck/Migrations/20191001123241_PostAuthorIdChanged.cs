using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityCheck.Migrations
{
    public partial class PostAuthorIdChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0ab5cbaa-6522-4d1a-b21c-ceba38b0071c", "96f8a790-915a-40a2-9338-7b23054e9244" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a0084da7-061f-4e59-92b8-348edaebf284", "8a076ada-a5f9-4070-93c5-d18b57bd4e6f" });

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Posts",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_ApplicationUserId",
                table: "Posts",
                newName: "IX_Posts_AuthorId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "61aaf8ee-143d-4e32-8de0-48fbb63ccad1", "67035405-642b-477d-a2b0-658372777026", "SuperUser", "SUPERUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8e50aa6b-08eb-45b5-8304-347526bac819", "b23a923a-7697-4d35-8ddf-e97023c73e88", "RegularUser", "REGULARUSER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "61aaf8ee-143d-4e32-8de0-48fbb63ccad1", "67035405-642b-477d-a2b0-658372777026" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8e50aa6b-08eb-45b5-8304-347526bac819", "b23a923a-7697-4d35-8ddf-e97023c73e88" });

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Posts",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                newName: "IX_Posts_ApplicationUserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a0084da7-061f-4e59-92b8-348edaebf284", "8a076ada-a5f9-4070-93c5-d18b57bd4e6f", "SuperUser", "SUPERUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ab5cbaa-6522-4d1a-b21c-ceba38b0071c", "96f8a790-915a-40a2-9338-7b23054e9244", "RegularUser", "REGULARUSER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
