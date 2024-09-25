using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class lastone2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isNewUser",
                table: "AspNetUsers",
                newName: "deActivated");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bea37cfa-51a9-4e03-a219-ed056d46b141", "AQAAAAIAAYagAAAAEPIB9D1pBnqkaEgPjosXSdHMmcdB8HlVOWa+N/BohqFwra8uq/9m71G/NosbsxXapA==", "c2ce4eea-05c1-48aa-800f-73f891a5c6ee" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deActivated",
                table: "AspNetUsers",
                newName: "isNewUser");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5cd1c0c7-7014-45c7-af65-88c3d7ba88d5", "AQAAAAIAAYagAAAAED8iEf2eXwTQ/xZlARiRNltSOTz2vIPIAQE0ykBp1WflkdUbXIIFcce8Q8/HtDd1fw==", "1c3917a9-dad1-4dde-aaab-d35a8eb05f45" });
        }
    }
}
