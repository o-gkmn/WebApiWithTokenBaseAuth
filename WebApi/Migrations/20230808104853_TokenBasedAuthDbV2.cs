using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class TokenBasedAuthDbV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0345b89e-acf0-4d00-be1a-1a185cf34cda");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f71587c-1e1b-4cc8-96a9-1b9d776ca5c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99fc1255-e621-4db5-a8be-e3e3afc19aa8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "1e10e0a8-432a-48d6-9dc6-0a72e976c58a", null, "Role", "Admin", "ADMIN", new DateTime(2023, 8, 8, 10, 48, 53, 53, DateTimeKind.Utc).AddTicks(193) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "5005842e-5c67-4be9-a602-6a700cac5d08", null, "Role", "User", "USER", new DateTime(2023, 8, 8, 10, 48, 53, 53, DateTimeKind.Utc).AddTicks(128) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "6d25863b-e096-476d-822f-26221839d52e", null, "Role", "Editor", "EDITOR", new DateTime(2023, 8, 8, 10, 48, 53, 53, DateTimeKind.Utc).AddTicks(188) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e10e0a8-432a-48d6-9dc6-0a72e976c58a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5005842e-5c67-4be9-a602-6a700cac5d08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d25863b-e096-476d-822f-26221839d52e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "0345b89e-acf0-4d00-be1a-1a185cf34cda", null, "Role", "Admin", "ADMIN", new DateTime(2023, 8, 7, 6, 46, 51, 298, DateTimeKind.Utc).AddTicks(6258) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "3f71587c-1e1b-4cc8-96a9-1b9d776ca5c4", null, "Role", "Editor", "EDITOR", new DateTime(2023, 8, 7, 6, 46, 51, 298, DateTimeKind.Utc).AddTicks(6251) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "99fc1255-e621-4db5-a8be-e3e3afc19aa8", null, "Role", "User", "USER", new DateTime(2023, 8, 7, 6, 46, 51, 298, DateTimeKind.Utc).AddTicks(6205) });
        }
    }
}
