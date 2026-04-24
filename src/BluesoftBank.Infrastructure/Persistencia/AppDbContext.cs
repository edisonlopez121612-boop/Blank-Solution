using BluesoftBank.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BluesoftBank.Infrastructure.Persistencia;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cuenta> Cuentas => Set<Cuenta>();
    public DbSet<Transaccion> Transacciones => Set<Transaccion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ================= CUENTA =================
        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(c => c.NumeroCuenta);

            entity.Property(c => c.NumeroCuenta)
                  .HasMaxLength(20)
                  .HasColumnType("TEXT")
                  .IsRequired();

            entity.Property(c => c.Saldo)
                  .HasColumnType("REAL")
                  .IsRequired();

            entity.Property(c => c.CiudadOrigen)
                  .HasMaxLength(50)
                  .HasColumnType("TEXT")
                  .IsRequired();

            entity.Property(c => c.RowVersion)
                  .IsRowVersion()
                  .IsConcurrencyToken();
        });

        // ================= TRANSACCION =================
        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Valor)
                  .HasColumnType("REAL")
                  .IsRequired();

            entity.Property(t => t.NumeroCuenta)
                  .HasMaxLength(20)
                  .HasColumnType("TEXT")
                  .IsRequired();

            entity.Property(t => t.CiudadOperacion)
                  .HasColumnType("TEXT")
                  .IsRequired();

            entity.Property(t => t.Fecha)
                  .IsRequired();

            entity.Property(t => t.TipoTransaccion)
         .HasConversion<int>()
         .IsRequired();

            entity.HasOne(t => t.Cuenta)
                  .WithMany(c => c.Transacciones)
                  .HasForeignKey(t => t.NumeroCuenta)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}