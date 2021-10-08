﻿// <auto-generated />
using System;
using GestioneSpese.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestioneSpese.Migrations
{
    [DbContext(typeof(GestioneSpeseContext))]
    partial class GestioneSpeseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestioneSpese.Library.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoriaID");

                    b.ToTable("Categorie");
                });

            modelBuilder.Entity("GestioneSpese.Library.Models.Spesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approvato")
                        .HasColumnType("bit");

                    b.Property<int?>("CategoriaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("Importo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Utente")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaID");

                    b.ToTable("Spese");
                });

            modelBuilder.Entity("GestioneSpese.Library.Models.Spesa", b =>
                {
                    b.HasOne("GestioneSpese.Library.Models.Categoria", "Categoria")
                        .WithMany("Spese")
                        .HasForeignKey("CategoriaID");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("GestioneSpese.Library.Models.Categoria", b =>
                {
                    b.Navigation("Spese");
                });
#pragma warning restore 612, 618
        }
    }
}
