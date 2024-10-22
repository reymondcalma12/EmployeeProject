using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wadwawa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Projects_ActivitiesStatus_Projects_ActivitiesStatusStatusId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_ActivitiesStatus_Projects_ActivitiesStatusStatusId",
                table: "Projects");

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
                name: "Projects_ActivitiesStatusStatusId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Projects",
                newName: "Projects_ActivitiesStatusId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Activities",
                newName: "Projects_ActivitiesStatusId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a69684b4-79f1-4c8b-95b7-b4bb1c5c8a5c", "AQAAAAIAAYagAAAAEIYzVaHoYc5jKV9HsFIS5LMHCxfs2okMRSuX0L+cEBENCTkFjZGLt2NPRalJHUST5A==", "53506fff-5a48-4477-8e10-0fd75112e99f" });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Projects_ActivitiesStatusId",
                table: "Projects",
                column: "Projects_ActivitiesStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Projects_ActivitiesStatusId",
                table: "Activities",
                column: "Projects_ActivitiesStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Projects_ActivitiesStatus_Projects_ActivitiesStatusId",
                table: "Activities",
                column: "Projects_ActivitiesStatusId",
                principalTable: "Projects_ActivitiesStatus",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Projects_ActivitiesStatus_Projects_ActivitiesStatusId",
                table: "Projects",
                column: "Projects_ActivitiesStatusId",
                principalTable: "Projects_ActivitiesStatus",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Projects_ActivitiesStatus_Projects_ActivitiesStatusId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_ActivitiesStatus_Projects_ActivitiesStatusId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Projects_ActivitiesStatusId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Activities_Projects_ActivitiesStatusId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Projects_ActivitiesStatusId",
                table: "Projects",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Projects_ActivitiesStatusId",
                table: "Activities",
                newName: "StatusId");

            migrationBuilder.AddColumn<int>(
                name: "Projects_ActivitiesStatusStatusId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Projects_ActivitiesStatusStatusId",
                table: "Activities",
                type: "int",
                nullable: true);

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
    }
}
