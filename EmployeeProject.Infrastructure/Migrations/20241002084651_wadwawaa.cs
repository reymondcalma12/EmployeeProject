using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wadwawaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91081229-0154-4875-8a72-64825e6f414d", "AQAAAAIAAYagAAAAECuIJ+LeJdOlUP7FyKpoGmkc2vK9dq+hEOViVGXW0ZzTO73TmytKvMMZknpiIEHcAg==", "1af1cfc5-f697-4bf1-b946-cd7785594f2f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a69684b4-79f1-4c8b-95b7-b4bb1c5c8a5c", "AQAAAAIAAYagAAAAEIYzVaHoYc5jKV9HsFIS5LMHCxfs2okMRSuX0L+cEBENCTkFjZGLt2NPRalJHUST5A==", "53506fff-5a48-4477-8e10-0fd75112e99f" });
        }
    }
}
