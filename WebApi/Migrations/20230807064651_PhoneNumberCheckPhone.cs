using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class PhoneNumberCheckPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94315c6f-6ca3-4428-aa7f-47b5704764b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e830a08-1d8f-4af7-a69a-901d0c7f6ca4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce80a5cf-fbce-488e-a3be-c45fb9cc2bba");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "94315c6f-6ca3-4428-aa7f-47b5704764b4", null, "Role", "User", "USER", new DateTime(2023, 8, 7, 6, 42, 4, 758, DateTimeKind.Utc).AddTicks(9053) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "9e830a08-1d8f-4af7-a69a-901d0c7f6ca4", null, "Role", "Admin", "ADMIN", new DateTime(2023, 8, 7, 6, 42, 4, 758, DateTimeKind.Utc).AddTicks(9116) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "ce80a5cf-fbce-488e-a3be-c45fb9cc2bba", null, "Role", "Editor", "EDITOR", new DateTime(2023, 8, 7, 6, 42, 4, 758, DateTimeKind.Utc).AddTicks(9107) });
        }
    }
}
