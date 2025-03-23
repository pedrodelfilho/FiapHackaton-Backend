using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class SolicitacaoExameMap : IEntityTypeConfiguration<SolicitacaoExame>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoExame> builder)
        {
            builder.ToTable("SolicitacaoExame");

            builder.Property(x => x.IdSolicitacao)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(x => x.IdExame)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.HasOne(x => x.SolicitacaoAgendamento)
                .WithMany()
                .HasForeignKey(x => x.IdSolicitacao);

            builder.HasOne(x => x.TiposExames)
                .WithMany()
                .HasForeignKey(x => x.IdExame);
        }
    }
}
