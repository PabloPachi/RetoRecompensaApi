using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPC.RetoRecompensa.Domain.Entities;

namespace PPC.RetoRecompensa.Infrastructure.Persistence.Configurations;

public class RetoUsuarioConfig : IEntityTypeConfiguration<RetoUsuario>
{
    public void Configure(EntityTypeBuilder<RetoUsuario> builder)
    {
        builder.ToTable("RetoUsuario");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Estado)
               .IsRequired();

        builder.Property(x => x.Creacion)
               .IsRequired();

        // 🔗 FK Reto
        builder.HasOne(x => x.Reto)
               .WithMany(r => r.Usuarios)
               .HasForeignKey(x => x.IdReto)
               .OnDelete(DeleteBehavior.Restrict);

        // 🔗 FK Usuario
        builder.HasOne(x => x.Usuario)
               .WithMany(u => u.Retos)
               .HasForeignKey(x => x.IdUsuario)
               .OnDelete(DeleteBehavior.Restrict);
    }
}