﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Database;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DeparterContext))]
    [Migration("20230226000303_alteracao-funcionario-horas")]
    partial class alteracaofuncionariohoras
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Atividade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("AtividadePaiId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_atividade_pai");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_criacao");

                    b.Property<DateTime>("DataEntrega")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_entrega");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_modificacao");

                    b.Property<Guid>("DepartamentoId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_departamento");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<int>("TempoPrevisto")
                        .HasColumnType("integer")
                        .HasColumnName("tempo_previsto");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("titulo");

                    b.HasKey("Id");

                    b.HasIndex("AtividadePaiId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("atividades");
                });

            modelBuilder.Entity("Domain.Entities.AtividadeCategoria", b =>
                {
                    b.Property<Guid>("AtividadeId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_atividade");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_category");

                    b.HasKey("AtividadeId", "CategoriaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("atividade_categorias");
                });

            modelBuilder.Entity("Domain.Entities.AtividadeCheck", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AtividadeId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_atividade");

                    b.Property<bool>("Checked")
                        .HasColumnType("boolean")
                        .HasColumnName("checked");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_criacao");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_modificacao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.HasKey("Id");

                    b.HasIndex("AtividadeId");

                    b.ToTable("atividade_check");
                });

            modelBuilder.Entity("Domain.Entities.AtividadeFuncionario", b =>
                {
                    b.Property<Guid>("AtividadeId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_atividade");

                    b.Property<Guid>("FuncionarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_funcionario");

                    b.Property<int>("NivelAcesso")
                        .HasColumnType("integer")
                        .HasColumnName("nivel_acesso");

                    b.HasKey("AtividadeId", "FuncionarioId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("atividade_funcionarios");
                });

            modelBuilder.Entity("Domain.Entities.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cor");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_criacao");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_modificacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("Domain.Entities.Departamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_criacao");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_modificacao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<int>("MaximoHorasDiarias")
                        .HasColumnType("integer")
                        .HasColumnName("maximo_horas_diarias");

                    b.Property<int>("MaximoHorasMensais")
                        .HasColumnType("integer")
                        .HasColumnName("maximo_horas_mensais");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.ToTable("departamentos");
                });

            modelBuilder.Entity("Domain.Entities.DepartamentoFuncionario", b =>
                {
                    b.Property<Guid>("FuncionarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_funcionario");

                    b.Property<Guid>("DepartamentoId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_departamento");

                    b.HasKey("FuncionarioId", "DepartamentoId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("departamento_funcionarios");
                });

            modelBuilder.Entity("Domain.Entities.Funcionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Apelido")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("apelido");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_criacao");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_modificacao");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("imagem");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("senha");

                    b.HasKey("Id");

                    b.ToTable("funcionarios");
                });

            modelBuilder.Entity("Domain.Entities.FuncionarioAtividadeHoras", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AtividadeId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_atividade");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_criacao");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_modificacao");

                    b.Property<Guid>("FuncionarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_funcionario");

                    b.Property<int>("Minutos")
                        .HasColumnType("integer")
                        .HasColumnName("minutos");

                    b.HasKey("Id");

                    b.HasIndex("AtividadeId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("funcionario_atividade_horas");
                });

            modelBuilder.Entity("Domain.Entities.FuncionarioHorasConfiguracao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_criacao");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_modificacao");

                    b.Property<Guid>("FuncionarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_funcionario");

                    b.Property<int>("Minutos")
                        .HasColumnType("integer")
                        .HasColumnName("minutos");

                    b.Property<int>("TipoConfiguracao")
                        .HasColumnType("integer")
                        .HasColumnName("tipo_configuracao");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("funcionario_horas_configuracoes");
                });

            modelBuilder.Entity("Domain.Entities.Atividade", b =>
                {
                    b.HasOne("Domain.Entities.Atividade", "AtividadePai")
                        .WithMany("Atividades")
                        .HasForeignKey("AtividadePaiId");

                    b.HasOne("Domain.Entities.Departamento", "Departamento")
                        .WithMany("Atividades")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AtividadePai");

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("Domain.Entities.AtividadeCategoria", b =>
                {
                    b.HasOne("Domain.Entities.Atividade", "Atividade")
                        .WithMany("AtividadeCategorias")
                        .HasForeignKey("AtividadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Categoria", "Categoria")
                        .WithMany("AtividadeCategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atividade");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Domain.Entities.AtividadeCheck", b =>
                {
                    b.HasOne("Domain.Entities.Atividade", "Atividade")
                        .WithMany("AtividadeChecks")
                        .HasForeignKey("AtividadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atividade");
                });

            modelBuilder.Entity("Domain.Entities.AtividadeFuncionario", b =>
                {
                    b.HasOne("Domain.Entities.Atividade", "Atividade")
                        .WithMany("AtividadeFuncionarios")
                        .HasForeignKey("AtividadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("AtividadeFuncionarios")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atividade");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Domain.Entities.DepartamentoFuncionario", b =>
                {
                    b.HasOne("Domain.Entities.Departamento", "Departamento")
                        .WithMany("DepartamentoFuncionarios")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("DepartamentoFuncionarios")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamento");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Domain.Entities.FuncionarioAtividadeHoras", b =>
                {
                    b.HasOne("Domain.Entities.Atividade", "Atividade")
                        .WithMany("FuncionarioAtividadeHoras")
                        .HasForeignKey("AtividadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("FuncionarioAtividadeHoras")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atividade");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Domain.Entities.FuncionarioHorasConfiguracao", b =>
                {
                    b.HasOne("Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("FuncionarioHorasConfiguracaos")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Domain.Entities.Atividade", b =>
                {
                    b.Navigation("AtividadeCategorias");

                    b.Navigation("AtividadeChecks");

                    b.Navigation("AtividadeFuncionarios");

                    b.Navigation("Atividades");

                    b.Navigation("FuncionarioAtividadeHoras");
                });

            modelBuilder.Entity("Domain.Entities.Categoria", b =>
                {
                    b.Navigation("AtividadeCategorias");
                });

            modelBuilder.Entity("Domain.Entities.Departamento", b =>
                {
                    b.Navigation("Atividades");

                    b.Navigation("DepartamentoFuncionarios");
                });

            modelBuilder.Entity("Domain.Entities.Funcionario", b =>
                {
                    b.Navigation("AtividadeFuncionarios");

                    b.Navigation("DepartamentoFuncionarios");

                    b.Navigation("FuncionarioAtividadeHoras");

                    b.Navigation("FuncionarioHorasConfiguracaos");
                });
#pragma warning restore 612, 618
        }
    }
}
