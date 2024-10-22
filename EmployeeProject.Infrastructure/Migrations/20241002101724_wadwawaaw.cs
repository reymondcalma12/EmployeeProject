using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wadwawaaw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23b76b5c-9f4e-47da-852c-469062f84819", "AQAAAAIAAYagAAAAELoKgeO+uai0xjc60ceB1G2MptQM7TPj9BlFaX7vSIGSsBv5X1JY+q9kSErwiazwlw==", "683b56e7-69d5-43bd-a623-e54cb913e86c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91081229-0154-4875-8a72-64825e6f414d", "AQAAAAIAAYagAAAAECuIJ+LeJdOlUP7FyKpoGmkc2vK9dq+hEOViVGXW0ZzTO73TmytKvMMZknpiIEHcAg==", "1af1cfc5-f697-4bf1-b946-cd7785594f2f" });
        }
    }
}
