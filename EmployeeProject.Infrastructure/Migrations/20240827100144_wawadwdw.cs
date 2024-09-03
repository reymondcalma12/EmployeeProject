using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wawadwdw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataSheetBuses_Sections_SectionId",
                table: "DataSheetBuses");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "DataSheetBuses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "af26cb1a-1ad7-4ad7-8ef9-e59822bdb69c", "AQAAAAIAAYagAAAAEFrADSmlkpxJjZ/Ym8WwkFJLLLxXmZU7eLC9mm1jXGQZFIbpvunidrinUQDFDZpRFw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_DataSheetBuses_Sections_SectionId",
                table: "DataSheetBuses",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataSheetBuses_Sections_SectionId",
                table: "DataSheetBuses");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "DataSheetBuses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "78289165-690c-4a2f-8251-3af05ccc00d0", "AQAAAAIAAYagAAAAECweJTBi0DKLno8KP55myqbyyqnN7SH6qPX5S+JscvYsTQp6nKq7eVbM2pQs7QlIxA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_DataSheetBuses_Sections_SectionId",
                table: "DataSheetBuses",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
