using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infra.Data.Mappings
{
    public class ResultadoMap : IEntityTypeConfiguration<Resultado>
    {
        public void Configure(EntityTypeBuilder<Resultado> builder)
        {
            builder.ToTable("Resultado");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT");

            builder.Property(x => x.IdSolicitacao)
                .IsRequired()
                .HasColumnName("Solicitacao")
                .HasColumnType("bigint");

            builder.Property(x => x.Arquivo)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Arquivo")
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Data)
                .IsRequired()
                .HasColumnName("Data")
                .HasColumnType("datetime");

            builder.Property(x => x.Atendente)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("Atendente")
                .HasColumnType("nvarchar(450)");

            builder.HasOne(x => x.Solicitacao)
                .WithMany()
                .HasForeignKey(x => x.IdSolicitacao);
        }
    }
}
