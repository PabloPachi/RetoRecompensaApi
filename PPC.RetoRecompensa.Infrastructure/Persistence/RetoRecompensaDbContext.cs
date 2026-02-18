using Microsoft.EntityFrameworkCore;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Infrastructure.Persistence;

public class RetoRecompensaDbContext : DbContext
{
    public RetoRecompensaDbContext(DbContextOptions<RetoRecompensaDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuario => Set<Usuario>();
    public DbSet<Reto> Reto => Set<Reto>();
    public DbSet<RetoUsuario> RetoUsuario => Set<RetoUsuario>();
    public DbSet<Recompensa> Recompensa => Set<Recompensa>();
    public DbSet<RecompensaUsuario> RecompensaUsuario => Set<RecompensaUsuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(RetoRecompensaDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
