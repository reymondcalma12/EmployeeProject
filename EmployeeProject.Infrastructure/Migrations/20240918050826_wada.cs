using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bf45ef1-c501-4c38-b427-202a98e45f79", "AQAAAAIAAYagAAAAEJeG2QsFFMr6cswoDeTwVI+ovi7NZxFEQ8FnDEP06WkpUwQV1G1uQQk46sRv/uW4aA==", "54cf82e5-3014-40df-851a-eec3a784e401" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Activities");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70688e2f-2a69-41a7-981f-01a205386a65", "AQAAAAIAAYagAAAAEMKSt1giJ6FpHLtTK8Dr6fP96ygkl4EQ+TokfrW/Wa8zaeiF4qT8E0iV5QzVM1MGLQ==", "4407f7ba-0f96-4861-b093-45f48f7f6cae" });
        }
    }
}
