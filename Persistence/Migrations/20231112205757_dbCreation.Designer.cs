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
    [Migration("20231112205757_dbCreation")]
    partial class dbCreation
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

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<Guid>("EquipeId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_equipe");

                    b.Property<int>("OnScreenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_onscreen");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OnScreenId"));

                    b.Property<int>("StatusAtividade")
                        .HasColumnType("integer")
                        .HasColumnName("status_tarefa");

                    b.Property<int>("TempoPrevisto")
                        .HasColumnType("integer")
                        .HasColumnName("tempo_previsto");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("titulo");

                    b.HasKey("Id");

                    b.HasIndex("AtividadePaiId");

                    b.HasIndex("EquipeId");

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

            modelBuilder.Entity("Domain.Entities.AtividadeUsuario", b =>
                {
                    b.Property<Guid>("AtividadeId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_atividade");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.Property<int>("NivelAcesso")
                        .HasColumnType("integer")
                        .HasColumnName("nivel_acesso");

                    b.HasKey("AtividadeId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("atividade_usuarios");
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

            modelBuilder.Entity("Domain.Entities.Equipe", b =>
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

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_url");

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

                    b.Property<int>("OnScreenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_onscreen");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OnScreenId"));

                    b.HasKey("Id");

                    b.ToTable("equipes");
                });

            modelBuilder.Entity("Domain.Entities.EquipeUsuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.Property<Guid>("EquipeId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_equipe");

                    b.HasKey("UsuarioId", "EquipeId");

                    b.HasIndex("EquipeId");

                    b.ToTable("equipe_usuarios");
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
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

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Imagem")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("imagem");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean")
                        .HasColumnName("is_admin");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("senha");

                    b.HasKey("Id");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("Domain.Entities.UsuarioAtividadeHoras", b =>
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

                    b.Property<int>("Minutos")
                        .HasColumnType("integer")
                        .HasColumnName("minutos");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.HasKey("Id");

                    b.HasIndex("AtividadeId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("usuario_atividade_horas");
                });

            modelBuilder.Entity("Domain.Entities.UsuarioHorasConfiguracao", b =>
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

                    b.Property<int>("Minutos")
                        .HasColumnType("integer")
                        .HasColumnName("minutos");

                    b.Property<int>("TipoConfiguracao")
                        .HasColumnType("integer")
                        .HasColumnName("tipo_configuracao");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("usuario_horas_configuracoes");
                });

            modelBuilder.Entity("Domain.Entities.Atividade", b =>
                {
                    b.HasOne("Domain.Entities.Atividade", "AtividadePai")
                        .WithMany("Atividades")
                        .HasForeignKey("AtividadePaiId");

                    b.HasOne("Domain.Entities.Equipe", "Equipe")
                        .WithMany("Atividades")
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AtividadePai");

                    b.Navigation("Equipe");
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

            modelBuilder.Entity("Domain.Entities.AtividadeUsuario", b =>
                {
                    b.HasOne("Domain.Entities.Atividade", "Atividade")
                        .WithMany("AtividadeUsuarios")
                        .HasForeignKey("AtividadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Usuario", "Usuario")
                        .WithMany("AtividadeUsuarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atividade");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Entities.EquipeUsuario", b =>
                {
                    b.HasOne("Domain.Entities.Equipe", "Equipe")
                        .WithMany("EquipeUsuarios")
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Usuario", "Usuario")
                        .WithMany("EquipeUsuarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipe");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Entities.UsuarioAtividadeHoras", b =>
                {
                    b.HasOne("Domain.Entities.Atividade", "Atividade")
                        .WithMany("UsuarioAtividadeHoras")
                        .HasForeignKey("AtividadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioAtividadeHoras")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atividade");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Entities.UsuarioHorasConfiguracao", b =>
                {
                    b.HasOne("Domain.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioHorasConfiguracaos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Entities.Atividade", b =>
                {
                    b.Navigation("AtividadeCategorias");

                    b.Navigation("AtividadeChecks");

                    b.Navigation("AtividadeUsuarios");

                    b.Navigation("Atividades");

                    b.Navigation("UsuarioAtividadeHoras");
                });

            modelBuilder.Entity("Domain.Entities.Categoria", b =>
                {
                    b.Navigation("AtividadeCategorias");
                });

            modelBuilder.Entity("Domain.Entities.Equipe", b =>
                {
                    b.Navigation("Atividades");

                    b.Navigation("EquipeUsuarios");
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.Navigation("AtividadeUsuarios");

                    b.Navigation("EquipeUsuarios");

                    b.Navigation("UsuarioAtividadeHoras");

                    b.Navigation("UsuarioHorasConfiguracaos");
                });
#pragma warning restore 612, 618
        }
    }
}