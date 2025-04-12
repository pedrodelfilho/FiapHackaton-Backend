using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class DisponibilidadeMedicoMap : IEntityTypeConfiguration<DisponibilidadeMedico>
    {
        public void Configure(EntityTypeBuilder<DisponibilidadeMedico> entity)
        {
            entity.ToTable("DisponibilidadeMedico");

            entity.HasKey(d => d.Id);

            entity.Property(d => d.Data)
                  .IsRequired();

            entity.Property(d => d.HoraInicio)
                  .IsRequired();

            entity.Property(d => d.HoraFim)
                  .IsRequired();

            entity.Property(d => d.Ativo)
                  .HasDefaultValue(true);

            entity.Property(d => d.MedicoId)
                  .IsRequired();

            entity.HasOne(d => d.Medico)
                  .WithMany(m => m.Disponibilidades)
                  .HasForeignKey(d => d.MedicoId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired();
        }
    }
}
