using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class funcionariohoras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "funcionario_atividade_horas",
                columns: table => new
                {
                    id_funcionario = table.Column<Guid>(type: "uuid", nullable: false),
                    id_atividade = table.Column<Guid>(type: "uuid", nullable: false),
                    minutos = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionario_atividade_horas", x => new { x.id_atividade, x.id_funcionario });
                    table.ForeignKey(
                        name: "FK_funcionario_atividade_horas_atividades_id_atividade",
                        column: x => x.id_atividade,
                        principalTable: "atividades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_funcionario_atividade_horas_funcionarios_id_funcionario",
                        column: x => x.id_funcionario,
                        principalTable: "funcionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "funcionario_horas_configuracoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_funcionario = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_configuracao = table.Column<int>(type: "integer", nullable: false),
                    minutos = table.Column<int>(type: "integer", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionario_horas_configuracoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_funcionario_horas_configuracoes_funcionarios_id_funcionario",
                        column: x => x.id_funcionario,
                        principalTable: "funcionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_atividade_horas_id_funcionario",
                table: "funcionario_atividade_horas",
                column: "id_funcionario");

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_horas_configuracoes_id_funcionario",
                table: "funcionario_horas_configuracoes",
                column: "id_funcionario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "funcionario_atividade_horas");

            migrationBuilder.DropTable(
                name: "funcionario_horas_configuracoes");
        }
    }
}
