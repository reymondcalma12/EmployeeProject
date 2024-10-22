using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wadwaw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "Projects_ActivitiesStatusStatusId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Projects_ActivitiesStatusStatusId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projects_ActivitiesStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects_ActivitiesStatus", x => x.StatusId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ccc2901-e4dc-40a6-beda-96a667aab50b", "AQAAAAIAAYagAAAAEIEvvManHOZqrgYYDq2cqQSFTpp3xeuLW/TIDzt+DLPbr7CiQf6gP2oAZN4iogZcEg==", "b6a5aef0-ed05-4a0a-8e09-b9eeaa0672ee" });


            migrationBuilder.CreateIndex(
                name: "IX_Projects_Projects_ActivitiesStatusStatusId",
                table: "Projects",
                column: "Projects_ActivitiesStatusStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Projects_ActivitiesStatusStatusId",
                table: "Activities",
                column: "Projects_ActivitiesStatusStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Projects_ActivitiesStatus_Projects_ActivitiesStatusStatusId",
                table: "Activities",
                column: "Projects_ActivitiesStatusStatusId",
                principalTable: "Projects_ActivitiesStatus",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Projects_ActivitiesStatus_Projects_ActivitiesStatusStatusId",
                table: "Projects",
                column: "Projects_ActivitiesStatusStatusId",
                principalTable: "Projects_ActivitiesStatus",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Projects_ActivitiesStatus_Projects_ActivitiesStatusStatusId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_ActivitiesStatus_Projects_ActivitiesStatusStatusId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Projects_ActivitiesStatus");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Projects_ActivitiesStatusStatusId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Activities_Projects_ActivitiesStatusStatusId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Projects_ActivitiesStatusStatusId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Projects_ActivitiesStatusStatusId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

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
                values: new object[] { "1e17eb40-e11a-4953-8fb8-d9e7b79fe01e", "AQAAAAIAAYagAAAAEOvIVRREUoWCSnY/zD3DD3LhKrdv+Yvb1kNhkGGie9R0mXZiSXTJ07X0XcdChJB8rw==", "3cd1deb1-249b-4640-9abd-065f1a9e299f" });
        }
    }
}
