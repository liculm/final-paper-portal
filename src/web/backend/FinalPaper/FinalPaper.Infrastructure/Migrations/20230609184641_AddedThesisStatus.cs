using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalPaper.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedThesisStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "Thesis");

            migrationBuilder.AddColumn<int>(
                name: "ThesisStatusTypeId",
                table: "Thesis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ThesisStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThesisStatusTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CourseTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Preddiplomski" },
                    { 2, "Diplomski" }
                });

            migrationBuilder.InsertData(
                table: "ThesisStatusTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Čeka odobrenje" },
                    { 2, "Odobreno" },
                    { 3, "Odbijeno" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThesisStatusTypes");

            migrationBuilder.DeleteData(
                table: "CourseTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CourseTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ThesisStatusTypeId",
                table: "Thesis");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "Thesis",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
