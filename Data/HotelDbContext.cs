using GestionHotelera.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionHotelera.Data;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
    }

    public DbSet<Hotel> Hoteles { get; set; }
    public DbSet<Bloque> Bloques { get; set; }
    public DbSet<Piso> Pisos { get; set; }
    public DbSet<TipoHabilitacion> TiposHabilitaciones { get; set; }
    public DbSet<Habilitacion> Habilitaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Hotel
        modelBuilder.Entity<Hotel>()
            .Property(h => h.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Hotel>()
            .Property(h => h.Direccion)
            .HasMaxLength(200);

        modelBuilder.Entity<Hotel>()
            .Property(h => h.Telefono)
            .HasMaxLength(20);

        // Configuración de Bloque
        modelBuilder.Entity<Bloque>()
            .Property(b => b.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Bloque>()
            .HasOne(b => b.Hotel)
            .WithMany(h => h.Bloques)
            .HasForeignKey(b => b.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuración de Piso
        modelBuilder.Entity<Piso>()
            .Property(p => p.Descripcion)
            .HasMaxLength(200);

        modelBuilder.Entity<Piso>()
            .HasOne(p => p.Bloque)
            .WithMany(b => b.Pisos)
            .HasForeignKey(p => p.BloqueId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuración de TipoHabilitación
        modelBuilder.Entity<TipoHabilitacion>()
            .Property(t => t.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<TipoHabilitacion>()
            .Property(t => t.Descripcion)
            .HasMaxLength(300);

        // Configuración de Habilitación
        modelBuilder.Entity<Habilitacion>()
            .Property(h => h.Estado)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Habilitacion>()
            .HasOne(h => h.Piso)
            .WithMany(p => p.Habilitaciones)
            .HasForeignKey(h => h.PisoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Habilitacion>()
            .HasOne(h => h.TipoHabilitacion)
            .WithMany(t => t.Habilitaciones)
            .HasForeignKey(h => h.TipoHabilitacionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
