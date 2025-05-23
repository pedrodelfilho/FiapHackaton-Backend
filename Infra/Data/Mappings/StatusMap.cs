﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infra.Data.Mappings
{
    public class StatusMap : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT");

            builder.Property(x => x.DsStatus)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("DsStatus")
                .HasColumnType("VARCHAR(100)");

            builder.HasMany(x => x.Consultas)
                .WithOne()
                .HasForeignKey(x => x.IdStatus)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
