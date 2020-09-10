﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using biblioteca_core_att.Models;

namespace biblioteca_core_att.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20200910011853_alteracao_usuario_funcionario")]
    partial class alteracao_usuario_funcionario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("biblioteca_core_att.Models.Entrega", b =>
                {
                    b.Property<int>("EntregaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LivroID")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("EntregaID");

                    b.HasIndex("LivroID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Entregas");
                });

            modelBuilder.Entity("biblioteca_core_att.Models.Funcionario", b =>
                {
                    b.Property<int>("FuncionarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("FuncionarioID");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("biblioteca_core_att.Models.Livro", b =>
                {
                    b.Property<int>("LivroID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Autor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Lancamento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("LivroID");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("biblioteca_core_att.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UsuarioID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("biblioteca_core_att.Models.Entrega", b =>
                {
                    b.HasOne("biblioteca_core_att.Models.Livro", "Livros")
                        .WithMany("Entregas")
                        .HasForeignKey("LivroID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("biblioteca_core_att.Models.Usuario", "Usuarios")
                        .WithMany("Entregas")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
