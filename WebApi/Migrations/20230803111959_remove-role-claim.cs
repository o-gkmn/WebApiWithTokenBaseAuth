using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class removeroleclaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65ded3d8-4fc2-46f7-ac1f-349850b6f02a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ff6219f-22d3-4d76-90af-1c421a7a5278");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd7c7060-24b3-41b9-84a7-2bc2b7cfd455");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AspNetRoleClaims");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoleClaims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "AspNetRoleClaims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "65ded3d8-4fc2-46f7-ac1f-349850b6f02a", null, "Role", "User", "USER", new DateTime(2023, 8, 3, 10, 59, 57, 819, DateTimeKind.Utc).AddTicks(4431) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "9ff6219f-22d3-4d76-90af-1c421a7a5278", null, "Role", "Editor", "EDITOR", new DateTime(2023, 8, 3, 10, 59, 57, 819, DateTimeKind.Utc).AddTicks(4478) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "fd7c7060-24b3-41b9-84a7-2bc2b7cfd455", null, "Role", "Admin", "ADMIN", new DateTime(2023, 8, 3, 10, 59, 57, 819, DateTimeKind.Utc).AddTicks(4487) });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
