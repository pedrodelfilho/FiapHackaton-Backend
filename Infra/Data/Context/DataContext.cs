﻿using Domain.Entities;
using Infra.Data.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class DataContext : IdentityDbContext<UserIdentity>
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionString");
            }
        }
        public virtual DbSet<Agendamento> Agendamentos { get; set; }
        public virtual DbSet<Convenio> Convenios { get; set; }
        public virtual DbSet<HistoricoAgendamento> HistoricoSolicitalcaos { get; set; }
        public virtual DbSet<Resultado> Resultados { get; set; }
        public virtual DbSet<SolicitacaoAgendamento> SolicitacaoAgendamentos { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TiposExame> TiposExames { get; set; }
        public virtual DbSet<SolicitacaoExame> SolicitacaoExames { get; set; }
        public virtual DbSet<Especialidade> Especialidades { get; set; }
        public virtual DbSet<DisponibilidadeMedico> DisponibilidadeMedicos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AgendamentoMap());
            builder.ApplyConfiguration(new ConvenioMap());
            builder.ApplyConfiguration(new HistoricoAgendamentoMap());
            builder.ApplyConfiguration(new ResultadoMap());
            builder.ApplyConfiguration(new SolicitacaoAgendamentoMap());
            builder.ApplyConfiguration(new StatusMap());
            builder.ApplyConfiguration(new TiposExameMap());
            builder.ApplyConfiguration(new SolicitacaoExameMap());
            builder.ApplyConfiguration(new EspecialidadeMap());
            builder.ApplyConfiguration(new DisponibilidadeMedicoMap());
            builder.Entity<UserIdentity>()
                   .HasOne(u => u.Especialidade)
                   .WithMany()
                   .HasForeignKey(u => u.IdEspecialidade)
                   .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);
        }
    }
}
