using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourSoft.DAL.Data.Migrations
{
    public partial class AddDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "516da18e-60a4-4e85-8a77-f7bf958618bb", "6d60805a-8247-441c-bff4-0e121a08c8c6", "Manage User", "MANAGE USER" },
                    { "7fe25997-e0f5-4d0d-aa41-00658d66279b", "0f5f6bdf-a169-4ddd-90ac-33dcea7c90fb", "Manage Sample", "MANAGE SAMPLE" }
                });

            migrationBuilder.UpdateData(
                table: "Samples",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 8, 13, 15, 45, 45, 903, DateTimeKind.Local).AddTicks(6885));

            migrationBuilder.UpdateData(
                table: "Samples",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 8, 13, 15, 45, 45, 903, DateTimeKind.Local).AddTicks(6896));

            migrationBuilder.UpdateData(
                table: "Samples",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2022, 8, 13, 15, 45, 45, 903, DateTimeKind.Local).AddTicks(6897));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "516da18e-60a4-4e85-8a77-f7bf958618bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fe25997-e0f5-4d0d-aa41-00658d66279b");

            migrationBuilder.UpdateData(
                table: "Samples",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 8, 13, 14, 54, 15, 787, DateTimeKind.Local).AddTicks(6092));

            migrationBuilder.UpdateData(
                table: "Samples",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 8, 13, 14, 54, 15, 787, DateTimeKind.Local).AddTicks(6107));

            migrationBuilder.UpdateData(
                table: "Samples",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2022, 8, 13, 14, 54, 15, 787, DateTimeKind.Local).AddTicks(6108));
        }
    }
}
