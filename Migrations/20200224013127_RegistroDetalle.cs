using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroDetalle.Migrations
{
    public partial class RegistroDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: true),
                    Cedula = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonaId);
                });

            migrationBuilder.CreateTable(
                name: "TelefonosDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonaId = table.Column<int>(nullable: false),
                    TipoTelefono = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    PersonasPersonaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefonosDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelefonosDetalle_Personas_PersonasPersonaId",
                        column: x => x.PersonasPersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelefonosDetalle_PersonasPersonaId",
                table: "TelefonosDetalle",
                column: "PersonasPersonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelefonosDetalle");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
