﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApp.Models;

namespace WebApp.Migrations
{
    [DbContext(typeof(ReportesContext))]
    partial class ReportesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Correo")
                        .HasColumnName("correo")
                        .HasColumnType("character varying(254)")
                        .HasMaxLength(254);

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("nombre")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NumeroIdentificacion")
                        .HasColumnName("numero_identificacion")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Telefono")
                        .HasColumnName("telefono")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<string>("TipoIdentificacion")
                        .HasColumnName("tipo_identificacion")
                        .HasColumnType("character varying(3)")
                        .HasMaxLength(3);

                    b.Property<string>("TipoPersona")
                        .HasColumnName("tipo_persona")
                        .HasColumnType("character varying(2)")
                        .HasMaxLength(2);

                    b.Property<DateTime>("Updated")
                        .HasColumnName("updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("NumeroIdentificacion")
                        .IsUnique();

                    b.ToTable("productos_persona");
                });

            modelBuilder.Entity("WebApp.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("nombre")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Updated")
                        .HasColumnName("updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("productos_categoria");
                });

            modelBuilder.Entity("WebApp.Models.Certificado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Fecha")
                        .HasColumnName("fecha")
                        .HasColumnType("date");

                    b.Property<string>("Observaciones")
                        .HasColumnName("observaciones")
                        .HasColumnType("text");

                    b.Property<int>("PersonaId")
                        .HasColumnName("persona_id")
                        .HasColumnType("integer");

                    b.Property<int>("ProductoId")
                        .HasColumnName("producto_id")
                        .HasColumnType("integer");

                    b.Property<Guid>("UUID")
                        .HasColumnName("uuid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnName("updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UUID")
                        .IsUnique();

                    b.ToTable("productos_certificado");
                });

            modelBuilder.Entity("WebApp.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("estado")
                        .HasColumnType("character varying(1)")
                        .HasMaxLength(1)
                        .HasDefaultValue("A");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("nombre")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Updated")
                        .HasColumnName("updated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("url")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("productos_curso");
                });

            modelBuilder.Entity("WebApp.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("estado")
                        .HasColumnType("character varying(1)")
                        .HasMaxLength(1)
                        .HasDefaultValue("A");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("nombre")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Updated")
                        .HasColumnName("updated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("productos_producto");
                });

            modelBuilder.Entity("WebApp.Models.ProductoCategorias", b =>
                {
                    b.Property<int>("ProductoId")
                        .HasColumnName("producto_id")
                        .HasColumnType("integer");

                    b.Property<int>("CategoriaId")
                        .HasColumnName("categoria_id")
                        .HasColumnType("integer");

                    b.HasKey("ProductoId", "CategoriaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("productos_producto_categorias");
                });

            modelBuilder.Entity("WebApp.Models.SolicitudCurso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CursoId")
                        .HasColumnName("curso_id")
                        .HasColumnType("integer");

                    b.Property<string>("Estado")
                        .HasColumnName("estado")
                        .HasColumnType("character varying(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Observaciones")
                        .HasColumnName("observaciones")
                        .HasColumnType("text");

                    b.Property<int>("PersonaId")
                        .HasColumnName("persona_id")
                        .HasColumnType("integer");

                    b.Property<Guid>("UUID")
                        .HasColumnName("uuid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnName("updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("PersonaId");

                    b.HasIndex("UUID")
                        .IsUnique();

                    b.ToTable("productos_solicitudcurso");
                });

            modelBuilder.Entity("WebApp.Models.SolicitudProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Estado")
                        .HasColumnName("estado")
                        .HasColumnType("character varying(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Observaciones")
                        .HasColumnName("observaciones")
                        .HasColumnType("text");

                    b.Property<int>("PersonaId")
                        .HasColumnName("persona_id")
                        .HasColumnType("integer");

                    b.Property<int>("ProductoId")
                        .HasColumnName("producto_id")
                        .HasColumnType("integer");

                    b.Property<Guid>("UUID")
                        .HasColumnName("uuid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Updated")
                        .HasColumnName("updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UUID")
                        .IsUnique();

                    b.ToTable("productos_solicitudproducto");
                });

            modelBuilder.Entity("WebApp.Models.Certificado", b =>
                {
                    b.HasOne("Persona", "Persona")
                        .WithMany("Certificados")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Producto", "Producto")
                        .WithMany("Certificados")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.ProductoCategorias", b =>
                {
                    b.HasOne("WebApp.Models.Categoria", "Categoria")
                        .WithMany("ProductosCategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Producto", "Producto")
                        .WithMany("ProductosCategorias")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.SolicitudCurso", b =>
                {
                    b.HasOne("WebApp.Models.Curso", "Curso")
                        .WithMany("SolicitudesCurso")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persona", "Persona")
                        .WithMany("SolicitudesCurso")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Models.SolicitudProducto", b =>
                {
                    b.HasOne("Persona", "Persona")
                        .WithMany("SolicitudesProducto")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Producto", "Producto")
                        .WithMany("SolicitudesProducto")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
