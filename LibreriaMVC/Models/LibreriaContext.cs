using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LibreriaMVC.Models
{
    public partial class LibreriaContext : DbContext
    {
        public LibreriaContext()
        {
        }

        public LibreriaContext(DbContextOptions<LibreriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.;database=Libreria;integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("cliente");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApMaterno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ap_materno");

                entity.Property(e => e.ApPaterno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ap_paterno");

                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("dni")
                    .IsFixedLength(true);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("genero")
                    .IsFixedLength(true);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombres");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("producto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("marca");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__producto__id_pro__286302EC");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("proveedor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Ruc)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("ruc")
                    .IsFixedLength(true);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("telefono")
                    .IsFixedLength(true);

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ubicacion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
