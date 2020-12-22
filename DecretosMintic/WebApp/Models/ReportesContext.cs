using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ReportesContext : DbContext
    {
        public ReportesContext(DbContextOptions<ReportesContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoCategorias> ProductosCategorias { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Certificado> Certificados { get; set; }
        public DbSet<SolicitudProducto> SolicitudProductos { get; set; }
        public DbSet<SolicitudCurso> SolicitudCursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().ToTable("productos_categoria");
            modelBuilder.Entity<Producto>().ToTable("productos_producto");
            modelBuilder.Entity<ProductoCategorias>().ToTable("productos_producto_categorias");
            modelBuilder.Entity<Persona>().ToTable("productos_persona");
            modelBuilder.Entity<Curso>().ToTable("productos_curso");
            modelBuilder.Entity<Certificado>().ToTable("productos_certificado");
            modelBuilder.Entity<SolicitudProducto>().ToTable("productos_solicitudproducto");
            modelBuilder.Entity<SolicitudCurso>().ToTable("productos_solicitudcurso");

            modelBuilder.Entity<ProductoCategorias>()
                .HasKey(
                    productoCategoria => new { productoCategoria.ProductoId, productoCategoria.CategoriaId }
                );

            modelBuilder.Entity<ProductoCategorias>()
                .HasOne(productoCategoria => productoCategoria.Producto)
                .WithMany(b => b.ProductosCategorias)
                .HasForeignKey(bc => bc.ProductoId);

            modelBuilder.Entity<ProductoCategorias>()
                .HasOne(productoCategoria => productoCategoria.Categoria)
                .WithMany(c => c.ProductosCategorias)
                .HasForeignKey(bc => bc.CategoriaId);

            modelBuilder.Entity<Producto>()
                .Property(b => b.Estado)
                .HasDefaultValue(ProductoEstado.Aprobado.Value);

            //Personas
            modelBuilder.Entity<Persona>()
                .HasIndex(e => e.NumeroIdentificacion)
                .IsUnique();
            
            //Cursos
            modelBuilder.Entity<Curso>()
               .Property(e => e.Estado)
               .HasDefaultValue(CursoEstado.Aprobado.Value);

            //Certificados
            modelBuilder.Entity<Certificado>()
                .Property(e => e.Observaciones)
                .HasColumnType("text");

            modelBuilder.Entity<Certificado>()
                .HasIndex(e => e.UUID)
                .IsUnique();

            modelBuilder.Entity<Certificado>()
                .HasOne(c => c.Persona)
                .WithMany(p => p.Certificados)
                .HasForeignKey(c => c.PersonaId);

            modelBuilder.Entity<Certificado>()
                .HasOne(c => c.Producto)
                .WithMany(p => p.Certificados)
                .HasForeignKey(c => c.ProductoId);

            //SolicitudProducto
            modelBuilder.Entity<SolicitudProducto>()
                .Property(e => e.Observaciones)
                .HasColumnType("text");

            modelBuilder.Entity<SolicitudProducto>()
                .HasOne(c => c.Persona)
                .WithMany(p => p.SolicitudesProducto)
                .HasForeignKey(c => c.PersonaId);

            modelBuilder.Entity<SolicitudProducto>()
                .HasOne(c => c.Producto)
                .WithMany(p => p.SolicitudesProducto)
                .HasForeignKey(c => c.ProductoId);

            modelBuilder.Entity<SolicitudProducto>()
                .HasIndex(e => e.UUID)
                .IsUnique();


            //SolicitudCurso
            modelBuilder.Entity<SolicitudCurso>()
                .Property(e => e.Observaciones)
                .HasColumnType("text");

            modelBuilder.Entity<SolicitudCurso>()
                .HasOne(c => c.Persona)
                .WithMany(p => p.SolicitudesCurso)
                .HasForeignKey(c => c.PersonaId);

            modelBuilder.Entity<SolicitudCurso>()
                .HasOne(c => c.Curso)
                .WithMany(p => p.SolicitudesCurso)
                .HasForeignKey(c => c.CursoId);

            modelBuilder.Entity<SolicitudCurso>()
                .HasIndex(e => e.UUID)
                .IsUnique();


        }

        
    }

}
