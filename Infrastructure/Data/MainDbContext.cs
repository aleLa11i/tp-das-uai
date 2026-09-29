using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options)
{
    public DbSet<Pintor> Pintores => Set<Pintor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pintor>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Nombre)
            .HasMaxLength(150)
            .IsRequired();

            entity.Property(p => p.Apellido)
            .HasMaxLength(150)
            .IsRequired();
        });
    }
}
