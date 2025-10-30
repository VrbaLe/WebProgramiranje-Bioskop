using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projekcije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Vreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sala = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Sifra = table.Column<long>(type: "bigint", nullable: false),
                    BrojReda = table.Column<long>(type: "bigint", nullable: false),
                    BrojSedistaURedu = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekcije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Karte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Red = table.Column<long>(type: "bigint", nullable: false),
                    Sediste = table.Column<long>(type: "bigint", nullable: false),
                    ProjekcijaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Karte_Projekcije_ProjekcijaId",
                        column: x => x.ProjekcijaId,
                        principalTable: "Projekcije",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Karte_ProjekcijaId",
                table: "Karte",
                column: "ProjekcijaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.DropTable(
                name: "Projekcije");
        }
    }
}
