using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wadwa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e17eb40-e11a-4953-8fb8-d9e7b79fe01e", "AQAAAAIAAYagAAAAEOvIVRREUoWCSnY/zD3DD3LhKrdv+Yvb1kNhkGGie9R0mXZiSXTJ07X0XcdChJB8rw==", "3cd1deb1-249b-4640-9abd-065f1a9e299f" });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AppUserId",
                table: "Projects",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AppUserId",
                table: "Projects",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AppUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AppUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Projects");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bf45ef1-c501-4c38-b427-202a98e45f79", "AQAAAAIAAYagAAAAEJeG2QsFFMr6cswoDeTwVI+ovi7NZxFEQ8FnDEP06WkpUwQV1G1uQQk46sRv/uW4aA==", "54cf82e5-3014-40df-851a-eec3a784e401" });
        }
    }
}
