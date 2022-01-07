using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Petalos.Models
{
    public partial class floresContext : DbContext
    {
        public floresContext()
        {
        }

        public floresContext(DbContextOptions<floresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DatosFloressssss> DatosFloressssss { get; set; }
        public virtual DbSet<Imagenesflores> Imagenesflores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=flores", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4");

            modelBuilder.Entity<DatosFloressssss>(entity =>
            {
                entity.HasKey(e => e.Idflor)
                    .HasName("PRIMARY");

                entity.ToTable("DatosFloressssss");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Idflor).HasColumnName("idflor");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreCientifico)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombrecientifico");

                entity.Property(e => e.NombreComun)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("nombrecomun");

                entity.Property(e => e.Origen)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("origen");
            });

            modelBuilder.Entity<Imagenesflores>(entity =>
            {
                entity.HasKey(e => e.IdImagen)
                    .HasName("PRIMARY");

                entity.ToTable("imagenesflores");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.IdFlor, "FK_imagenesflores_1");

                entity.Property(e => e.IdImagen).HasColumnName("IdImagen");

                entity.Property(e => e.IdFlor).HasColumnName("idflor");

                entity.Property(e => e.NombreImagen)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("NombreImagen");

                entity.HasOne(d => d.IdFlorNavigation)
                    .WithMany(p => p.ImagenesFlores)
                    .HasForeignKey(d => d.IdFlor)
                    .HasConstraintName("FK_imagenesflores_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
