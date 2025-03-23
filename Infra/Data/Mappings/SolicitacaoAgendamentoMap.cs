using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infra.Data.Mappings
{
    public class SolicitacaoAgendamentoMap : IEntityTypeConfiguration<SolicitacaoAgendamento>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoAgendamento> builder)
        {
            builder.ToTable("SolicitacaoAgendamento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT");

            builder.Property(x => x.Paciente)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("Paciente")
                .HasColumnType("NVARCHAR(450)");

            builder.Property(x => x.IdConvenio)
                .IsRequired()
                .HasColumnName("Convenio")
                .HasColumnType("bigint");

            builder.Property(x => x.Arquivo)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("Arquivo")
                .HasColumnType("VARCHAR(450)");

            builder.Property(x => x.Data)
                .IsRequired()
                .HasColumnName("Data")
                .HasColumnType("DateTime");

            builder.Property(x => x.IdStatus)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("bigint");

            builder.HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.IdStatus);

            builder.HasOne(x => x.Convenio)
                .WithMany()
                .HasForeignKey(x => x.IdConvenio);
        }
    }
}
