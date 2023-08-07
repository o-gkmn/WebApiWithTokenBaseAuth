using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class PhoneNumberCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "252cc335-7d65-4b8e-8f17-85e25772c18b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9ea8058-76ca-43ec-a70d-a061faeb399a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee153c7e-6b7e-4f53-a26a-925893aa7881");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "252cc335-7d65-4b8e-8f17-85e25772c18b", null, "Role", "User", "USER", new DateTime(2023, 8, 3, 11, 19, 59, 63, DateTimeKind.Utc).AddTicks(5526) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "e9ea8058-76ca-43ec-a70d-a061faeb399a", null, "Role", "Editor", "EDITOR", new DateTime(2023, 8, 3, 11, 19, 59, 63, DateTimeKind.Utc).AddTicks(5573) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "ee153c7e-6b7e-4f53-a26a-925893aa7881", null, "Role", "Admin", "ADMIN", new DateTime(2023, 8, 3, 11, 19, 59, 63, DateTimeKind.Utc).AddTicks(5578) });
        }
    }
}
