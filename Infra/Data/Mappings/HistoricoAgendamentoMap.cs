using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infra.Data.Mappings
{
    public class HistoricoAgendamentoMap : IEntityTypeConfiguration<HistoricoAgendamento>
    {
        public void Configure(EntityTypeBuilder<HistoricoAgendamento> builder)
        {
            builder.ToTable("HistoricoAgendamento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT");

            builder.Property(x => x.IdSolicitacao)
                .IsRequired()
                .HasColumnName("Solicitacao")
                .HasColumnType("bigint");

            builder.Property(x => x.IdStatus)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("bigint");

            builder.Property(x => x.Data)
                .IsRequired()
                .HasColumnName("Data")
                .HasColumnType("datetime");

            builder.Property(x => x.Atendente)
                .HasMaxLength(450)
                .HasColumnName("Atendente")
                .HasColumnType("NVARCHAR(450)");

            builder.HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.IdStatus);

            builder.HasOne(x => x.Solicitacao)
                .WithMany()
                .HasForeignKey(x => x.IdSolicitacao);
        }
    }
}
