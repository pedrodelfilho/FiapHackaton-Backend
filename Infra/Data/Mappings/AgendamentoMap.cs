using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class AgendamentoMap : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("Agendamento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT");

            builder.Property(x => x.IdSolicitacao)
                .IsRequired()
                .HasColumnName("Solicitacao")
                .HasColumnType("bigint");

            builder.Property(x => x.Data)
                .IsRequired()
                .HasColumnName("Data")
                .HasColumnType("datetime");

            builder.Property(x => x.IdStatus)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("bigint");

            builder.HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.IdStatus);

            builder.HasOne(x => x.Solicitacao)
                .WithMany()
                .HasForeignKey(x => x.IdSolicitacao);
        }
    }
}
