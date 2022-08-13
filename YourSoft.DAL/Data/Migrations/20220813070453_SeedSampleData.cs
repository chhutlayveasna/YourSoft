using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourSoft.DAL.Data.Migrations
{
    public partial class SeedSampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Samples",
                columns: new[] { "Id", "Date", "IsActive", "Name" },
                values: new object[] { 1, new DateTime(2022, 8, 13, 14, 4, 53, 791, DateTimeKind.Local).AddTicks(5588), true, "Sample Data 1" });

            migrationBuilder.InsertData(
                table: "Samples",
                columns: new[] { "Id", "Date", "IsActive", "Name" },
                values: new object[] { 2, new DateTime(2022, 8, 13, 14, 4, 53, 791, DateTimeKind.Local).AddTicks(5601), true, "Sample Data 2" });

            migrationBuilder.InsertData(
                table: "Samples",
                columns: new[] { "Id", "Date", "IsActive", "Name" },
                values: new object[] { 3, new DateTime(2022, 8, 13, 14, 4, 53, 791, DateTimeKind.Local).AddTicks(5602), true, "Sample Data 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Samples",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Samples",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Samples",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
