using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class alteracaoatividadedepartamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "departamento_atividades");

            migrationBuilder.AddColumn<Guid>(
                name: "id_departamento",
                table: "atividades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_atividades_id_departamento",
                table: "atividades",
                column: "id_departamento");

            migrationBuilder.AddForeignKey(
                name: "FK_atividades_departamentos_id_departamento",
                table: "atividades",
                column: "id_departamento",
                principalTable: "departamentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_atividades_departamentos_id_departamento",
                table: "atividades");

            migrationBuilder.DropIndex(
                name: "IX_atividades_id_departamento",
                table: "atividades");

            migrationBuilder.DropColumn(
                name: "id_departamento",
                table: "atividades");

            migrationBuilder.CreateTable(
                name: "departamento_atividades",
                columns: table => new
                {
                    id_atividade = table.Column<Guid>(type: "uuid", nullable: false),
                    id_departamento = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departamento_atividades", x => new { x.id_atividade, x.id_departamento });
                    table.ForeignKey(
                        name: "FK_departamento_atividades_atividades_id_atividade",
                        column: x => x.id_atividade,
                        principalTable: "atividades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_departamento_atividades_departamentos_id_departamento",
                        column: x => x.id_departamento,
                        principalTable: "departamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_departamento_atividades_id_departamento",
                table: "departamento_atividades",
                column: "id_departamento");
        }
    }
}
