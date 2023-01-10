using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class departamentocreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departamentos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    maximo_horas_diarias = table.Column<int>(type: "integer", nullable: false),
                    maximo_horas_mensais = table.Column<int>(type: "integer", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departamentos", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "departamento_funcionarios",
                columns: table => new
                {
                    id_departamento = table.Column<Guid>(type: "uuid", nullable: false),
                    id_funcionario = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departamento_funcionarios", x => new { x.id_funcionario, x.id_departamento });
                    table.ForeignKey(
                        name: "FK_departamento_funcionarios_departamentos_id_departamento",
                        column: x => x.id_departamento,
                        principalTable: "departamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_departamento_funcionarios_funcionarios_id_funcionario",
                        column: x => x.id_funcionario,
                        principalTable: "funcionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_departamento_atividades_id_departamento",
                table: "departamento_atividades",
                column: "id_departamento");

            migrationBuilder.CreateIndex(
                name: "IX_departamento_funcionarios_id_departamento",
                table: "departamento_funcionarios",
                column: "id_departamento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "departamento_atividades");

            migrationBuilder.DropTable(
                name: "departamento_funcionarios");

            migrationBuilder.DropTable(
                name: "departamentos");
        }
    }
}
