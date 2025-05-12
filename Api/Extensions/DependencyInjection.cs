using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Request;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infra.Data.Context;
using Infra.Data.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection strings
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BdPadraoConnection")));

            // JWT
            var jwtOptions = configuration.GetSection("JwtOptions");
            services.Configure<JwtOptions>(jwtOptions);

            // Identity
            services.AddDefaultIdentity<UserIdentity>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            // Definição do arquivo do request
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 5 * 1024 * 1024;
                options.MemoryBufferThreshold = int.MaxValue;
                options.ValueLengthLimit = int.MaxValue;
                options.BufferBodyLengthLimit = long.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });


            //Auto Mapper
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SolicitacaoConsultaRequest, Consulta>().ReverseMap();
                cfg.CreateMap<SolicitacaoAgendamentoUpdateStatusRequest, Consulta>().ReverseMap();
                cfg.CreateMap<DisponibilidadeMedicoRequest, DisponibilidadeMedico>().ReverseMap();
            });
            services.AddSingleton(autoMapperConfig.CreateMapper());

            // Repositórios
            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IEspecialidadeRepository, EspecialidadeRespository>();
            services.AddScoped<IDisponibilidadeMedicoRepository, DisponibilidadeMedicoRepository>();


            // Services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IConsultaService, ConsultaService>();
            services.AddScoped<IMedicoService, MedicoService>();

            return services;
        }

        public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = ["Paciente", "Medico", "Administrador"];

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
