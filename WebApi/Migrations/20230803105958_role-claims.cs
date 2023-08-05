using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class roleclaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18666030-0478-46b1-802a-424fa6a8cf3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42d603a2-1def-4db4-8949-dbb1793e435b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "479cd3ba-39be-422f-9dc1-97c5e42db94f");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "18666030-0478-46b1-802a-424fa6a8cf3b", null, "Role", "Editor", "EDITOR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "42d603a2-1def-4db4-8949-dbb1793e435b", null, "Role", "Admin", "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "createdAt" },
                values: new object[] { "479cd3ba-39be-422f-9dc1-97c5e42db94f", null, "Role", "User", "USER", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
