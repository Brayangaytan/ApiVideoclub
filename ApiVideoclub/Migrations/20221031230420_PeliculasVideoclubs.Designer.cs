﻿// <auto-generated />
using ApiVideoclub;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiVideoclub.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221031230420_PeliculasVideoclubs")]
    partial class PeliculasVideoclubs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApiVideoclub.Entidades.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Peliculas");
                });

            modelBuilder.Entity("ApiVideoclub.Entidades.PeliculaVideoclub", b =>
                {
                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("VideoclubId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.HasKey("PeliculaId", "VideoclubId");

                    b.HasIndex("VideoclubId");

                    b.ToTable("PeliculaVideoclub");
                });

            modelBuilder.Entity("ApiVideoclub.Entidades.Reseña", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Contenido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VideoclubId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VideoclubId");

                    b.ToTable("Reseñas");
                });

            modelBuilder.Entity("ApiVideoclub.Entidades.Videoclub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Videoclubs");
                });

            modelBuilder.Entity("ApiVideoclub.Entidades.PeliculaVideoclub", b =>
                {
                    b.HasOne("ApiVideoclub.Entidades.Pelicula", "Pelicula")
                        .WithMany("PeliculaVideoclub")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiVideoclub.Entidades.Videoclub", "Videoclub")
                        .WithMany("PeliculaVideoclub")
                        .HasForeignKey("VideoclubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pelicula");

                    b.Navigation("Videoclub");
                });

            modelBuilder.Entity("ApiVideoclub.Entidades.Reseña", b =>
                {
                    b.HasOne("ApiVideoclub.Entidades.Videoclub", "Videoclub")
                        .WithMany("Reseñas")
                        .HasForeignKey("VideoclubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Videoclub");
                });

            modelBuilder.Entity("ApiVideoclub.Entidades.Pelicula", b =>
                {
                    b.Navigation("PeliculaVideoclub");
                });

            modelBuilder.Entity("ApiVideoclub.Entidades.Videoclub", b =>
                {
                    b.Navigation("PeliculaVideoclub");

                    b.Navigation("Reseñas");
                });
#pragma warning restore 612, 618
        }
    }
}
