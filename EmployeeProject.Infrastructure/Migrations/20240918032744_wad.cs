using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AllowedHours",
                keyColumn: "AllowedHoursId",
                keyValue: 17);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70688e2f-2a69-41a7-981f-01a205386a65", "AQAAAAIAAYagAAAAEMKSt1giJ6FpHLtTK8Dr6fP96ygkl4EQ+TokfrW/Wa8zaeiF4qT8E0iV5QzVM1MGLQ==", "4407f7ba-0f96-4861-b093-45f48f7f6cae" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AllowedHours",
                columns: new[] { "AllowedHoursId", "Number" },
                values: new object[] { 17, 8.5f });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bea37cfa-51a9-4e03-a219-ed056d46b141", "AQAAAAIAAYagAAAAEPIB9D1pBnqkaEgPjosXSdHMmcdB8HlVOWa+N/BohqFwra8uq/9m71G/NosbsxXapA==", "c2ce4eea-05c1-48aa-800f-73f891a5c6ee" });
        }
    }
}
