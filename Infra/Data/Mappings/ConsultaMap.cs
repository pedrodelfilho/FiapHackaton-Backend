using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infra.Data.Mappings
{
    public class ConsultaMap : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("Consulta");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT");

            builder.Property(x => x.IdUsuarioPaciente)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("IdPaciente")
                .HasColumnType("NVARCHAR(450)");

            builder.Property(x => x.IdUsuarioMedico)
                .IsRequired()
                .HasColumnName("IdMedico")
                .HasColumnType("NVARCHAR(450)");

            builder.Property(x => x.IdDisponibilidade)
                .IsRequired()
                .HasColumnName("IdDisponibilidade")
                .HasColumnType("bigint");

            builder.Property(x => x.IdStatus)
                .IsRequired()
                .HasColumnName("IdStatus")
                .HasColumnType("bigint");

            builder.Property(x => x.DataSolicitacao)
                .IsRequired()
                .HasColumnName("Data")
                .HasColumnType("DateTime");

        }
    }
}
