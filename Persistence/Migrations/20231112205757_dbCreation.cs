using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    public partial class dbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    cor = table.Column<string>(type: "text", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    maximo_horas_diarias = table.Column<int>(type: "integer", nullable: false),
                    maximo_horas_mensais = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    id_onscreen = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    imagem = table.Column<string>(type: "text", nullable: false),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

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
                    id_equipe = table.Column<Guid>(type: "uuid", nullable: false),
                    status_tarefa = table.Column<int>(type: "integer", nullable: false),
                    id_onscreen = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.ForeignKey(
                        name: "FK_atividades_equipes_id_equipe",
                        column: x => x.id_equipe,
                        principalTable: "equipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "equipe_usuarios",
                columns: table => new
                {
                    id_equipe = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipe_usuarios", x => new { x.id_usuario, x.id_equipe });
                    table.ForeignKey(
                        name: "FK_equipe_usuarios_equipes_id_equipe",
                        column: x => x.id_equipe,
                        principalTable: "equipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipe_usuarios_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_horas_configuracoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_configuracao = table.Column<int>(type: "integer", nullable: false),
                    minutos = table.Column<int>(type: "integer", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_horas_configuracoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuario_horas_configuracoes_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
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

            migrationBuilder.CreateTable(
                name: "atividade_check",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    @checked = table.Column<bool>(name: "checked", type: "boolean", nullable: false),
                    id_atividade = table.Column<Guid>(type: "uuid", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade_check", x => x.id);
                    table.ForeignKey(
                        name: "FK_atividade_check_atividades_id_atividade",
                        column: x => x.id_atividade,
                        principalTable: "atividades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "atividade_usuarios",
                columns: table => new
                {
                    id_atividade = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    nivel_acesso = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade_usuarios", x => new { x.id_atividade, x.id_usuario });
                    table.ForeignKey(
                        name: "FK_atividade_usuarios_atividades_id_atividade",
                        column: x => x.id_atividade,
                        principalTable: "atividades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_atividade_usuarios_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_atividade_horas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    id_atividade = table.Column<Guid>(type: "uuid", nullable: false),
                    minutos = table.Column<int>(type: "integer", nullable: false),
                    dt_modificacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    dt_criacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_atividade_horas", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuario_atividade_horas_atividades_id_atividade",
                        column: x => x.id_atividade,
                        principalTable: "atividades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuario_atividade_horas_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_atividade_categorias_id_category",
                table: "atividade_categorias",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_atividade_check_id_atividade",
                table: "atividade_check",
                column: "id_atividade");

            migrationBuilder.CreateIndex(
                name: "IX_atividade_usuarios_id_usuario",
                table: "atividade_usuarios",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_atividades_id_atividade_pai",
                table: "atividades",
                column: "id_atividade_pai");

            migrationBuilder.CreateIndex(
                name: "IX_atividades_id_equipe",
                table: "atividades",
                column: "id_equipe");

            migrationBuilder.CreateIndex(
                name: "IX_equipe_usuarios_id_equipe",
                table: "equipe_usuarios",
                column: "id_equipe");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_atividade_horas_id_atividade",
                table: "usuario_atividade_horas",
                column: "id_atividade");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_atividade_horas_id_usuario",
                table: "usuario_atividade_horas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_horas_configuracoes_id_usuario",
                table: "usuario_horas_configuracoes",
                column: "id_usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atividade_categorias");

            migrationBuilder.DropTable(
                name: "atividade_check");

            migrationBuilder.DropTable(
                name: "atividade_usuarios");

            migrationBuilder.DropTable(
                name: "equipe_usuarios");

            migrationBuilder.DropTable(
                name: "usuario_atividade_horas");

            migrationBuilder.DropTable(
                name: "usuario_horas_configuracoes");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "atividades");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "equipes");
        }
    }
}
