using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Infrastructure.Persistence.Configurations;

public class RecompensaUsuarioConfig : IEntityTypeConfiguration<RecompensaUsuario>
{
    public void Configure(EntityTypeBuilder<RecompensaUsuario> builder)
    {
        builder.ToTable("RecompensaUsuario");

        builder.HasKey(x => x.Id);

        // 🔗 FK Reto
        builder.HasOne(x => x.Recompensa)
               .WithMany(r => r.Usuarios)
               .HasForeignKey(x => x.IdRecompensa)
               .OnDelete(DeleteBehavior.Restrict);

        // 🔗 FK Usuario
        builder.HasOne(x => x.Usuario)
               .WithMany(u => u.Recompensas)
               .HasForeignKey(x => x.IdUsuario)
               .OnDelete(DeleteBehavior.Restrict);
    }
}