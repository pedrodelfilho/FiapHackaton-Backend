using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infra.Data.Mappings
{
    public class TiposExameMap : IEntityTypeConfiguration<TiposExame>
    {
        public void Configure(EntityTypeBuilder<TiposExame> builder)
        {
            builder.ToTable("TiposExame");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Nome")
                .HasColumnType("VARCHAR(100)");
        }
    }
}
