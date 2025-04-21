using Domain.Entities;
using Infra.Data.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

        public virtual DbSet<Especialidade> Especialidades { get; set; }
        public virtual DbSet<DisponibilidadeMedico> DisponibilidadeMedicos { get; set; }
        public virtual DbSet<Consulta> Consultas { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ConsultaMap());
            builder.ApplyConfiguration(new StatusMap());
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
