using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class alteracaofuncionariohoras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_funcionario_atividade_horas",
                table: "funcionario_atividade_horas");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "funcionario_atividade_horas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "dt_criacao",
                table: "funcionario_atividade_horas",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dt_modificacao",
                table: "funcionario_atividade_horas",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_funcionario_atividade_horas",
                table: "funcionario_atividade_horas",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_atividade_horas_id_atividade",
                table: "funcionario_atividade_horas",
                column: "id_atividade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_funcionario_atividade_horas",
                table: "funcionario_atividade_horas");

            migrationBuilder.DropIndex(
                name: "IX_funcionario_atividade_horas_id_atividade",
                table: "funcionario_atividade_horas");

            migrationBuilder.DropColumn(
                name: "id",
                table: "funcionario_atividade_horas");

            migrationBuilder.DropColumn(
                name: "dt_criacao",
                table: "funcionario_atividade_horas");

            migrationBuilder.DropColumn(
                name: "dt_modificacao",
                table: "funcionario_atividade_horas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_funcionario_atividade_horas",
                table: "funcionario_atividade_horas",
                columns: new[] { "id_atividade", "id_funcionario" });
        }
    }
}
