using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class lastone1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5cd1c0c7-7014-45c7-af65-88c3d7ba88d5", "AQAAAAIAAYagAAAAED8iEf2eXwTQ/xZlARiRNltSOTz2vIPIAQE0ykBp1WflkdUbXIIFcce8Q8/HtDd1fw==", "1c3917a9-dad1-4dde-aaab-d35a8eb05f45" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb54dcbd-55eb-46dd-bd5f-f52acd2f4966", "AQAAAAIAAYagAAAAEKFrBQwE/iSKo2TzygHyPTzHo2UHIxKKiMZVjeXvuQ0pRWvQr1MBySp7bTNYiN+pKg==", "" });
        }
    }
}
