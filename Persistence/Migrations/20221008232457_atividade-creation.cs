using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class atividadecreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "atividades",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    dt_entrega = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    tempo_previsto = table.Column<int>(type: "integer", nullable: false),
                    id_atividade_pai = table.Column<Guid>(type: "uuid", nullable: true),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividades", x => x.id);
                    table.ForeignKey(
                        name: "FK_atividades_atividades_id_atividade_pai",
                        column: x => x.id_atividade_pai,
                        principalTable: "atividades",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "atividade_funcionarios",
                columns: table => new
                {
                    id_atividade = table.Column<Guid>(type: "uuid", nullable: false),
                    id_funcionario = table.Column<Guid>(type: "uuid", nullable: false),
                    nivel_acesso = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade_funcionarios", x => new { x.id_atividade, x.id_funcionario });
                    table.ForeignKey(
                        name: "FK_atividade_funcionarios_atividades_id_atividade",
                        column: x => x.id_atividade,
                        principalTable: "atividades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_atividade_funcionarios_funcionarios_id_funcionario",
                        column: x => x.id_funcionario,
                        principalTable: "funcionarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "atividade_categorias",
                columns: table => new
                {
                    id_atividade = table.Column<Guid>(type: "uuid", nullable: false),
                    id_category = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade_categorias", x => new { x.id_atividade, x.id_category });
                    table.ForeignKey(
                        name: "FK_atividade_categorias_atividades_id_atividade",
                        column: x => x.id_atividade,
                        principalTable: "atividades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_atividade_categorias_categorias_id_category",
                        column: x => x.id_category,
                        principalTable: "categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_atividade_categorias_id_category",
                table: "atividade_categorias",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_atividade_funcionarios_id_funcionario",
                table: "atividade_funcionarios",
                column: "id_funcionario");

            migrationBuilder.CreateIndex(
                name: "IX_atividades_id_atividade_pai",
                table: "atividades",
                column: "id_atividade_pai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atividade_categorias");

            migrationBuilder.DropTable(
                name: "atividade_funcionarios");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "atividades");
        }
    }
}
