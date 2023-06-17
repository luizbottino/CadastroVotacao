﻿// <auto-generated />
using System;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entidades.Migrations
{
    [DbContext(typeof(EntidadesContext))]
    [Migration("20230616231526_Atualização_da_tabela_voto_para_receber_nota")]
    partial class Atualização_da_tabela_voto_para_receber_nota
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entidades.Poema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TotalVotos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Poema");
                });

            modelBuilder.Entity("Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Role")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UF")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Entidades.Voto", b =>
                {
                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("IdPoema")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime");

                    b.Property<int>("Nota")
                        .HasColumnType("integer");

                    b.HasKey("IdUsuario", "IdPoema");

                    b.HasIndex("IdPoema");

                    b.ToTable("Voto");
                });

            modelBuilder.Entity("Entidades.Poema", b =>
                {
                    b.HasOne("Entidades.Usuario", "Usuario")
                        .WithMany("Poemas")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entidades.Voto", b =>
                {
                    b.HasOne("Entidades.Poema", "Poema")
                        .WithMany("Votos")
                        .HasForeignKey("IdPoema")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Entidades.Usuario", "Usuario")
                        .WithMany("Votos")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
