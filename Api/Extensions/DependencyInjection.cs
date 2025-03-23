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
using Constants = Application.Resource.Constants;

namespace Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection strings
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            // JWT
            var jwtOptions = configuration.GetSection("JwtOptions");
            services.Configure<JwtOptions>(jwtOptions);

            // Blob Storage
            var blobStorageSection = configuration.GetSection("BlobStorage");
            services.Configure<BlobStorage>(blobStorageSection);

            // Identity
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            // Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOrAtendente", policy =>
                {
                    policy.RequireRole(Constants.Admin, Constants.Atendente);
                });
            });

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
                cfg.CreateMap<StatusRequest, Status>().ReverseMap();
                cfg.CreateMap<StatusUpdateRequest, Status>().ReverseMap();
                cfg.CreateMap<ConvenioRequest, Convenio>().ReverseMap();
                cfg.CreateMap<ConvenioUpdateRequest, Convenio>().ReverseMap();
                cfg.CreateMap<TiposExameRequest, TiposExame>().ReverseMap();
                cfg.CreateMap<TiposExameUpdateRequest, TiposExame>().ReverseMap();
                cfg.CreateMap<SolicitacaoAgendamentoRequest, SolicitacaoAgendamento>().ReverseMap();
                cfg.CreateMap<SolicitacaoAgendamentoUpdateStatusRequest, SolicitacaoAgendamento>().ReverseMap();
                cfg.CreateMap<AgendamentoRequest, Agendamento>().ReverseMap();
            });
            services.AddSingleton(autoMapperConfig.CreateMapper());

            // Repositórios
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IConvenioRepository, ConvenioRepository>();
            services.AddScoped<ITiposExameRepository, TiposExameRepository>();
            services.AddScoped<ISolicitacaoAgendamentoRepository, SolicitacaoAgendamentoRepository>();
            services.AddScoped<IHistoricoAgendamentoRepository, HistoricoAgendamentoRepository>();
            services.AddScoped<ISolicitacaoExameRepository, SolicitacaoExameRepository>();
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

            // Services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IConvenioService, ConvenioService>();
            services.AddScoped<ITiposExameService, TiposExameService>();
            services.AddScoped<ISolicitacaoAgendamentoService, SolicitacaoAgendamentoService>();
            services.AddScoped<IHistoricoAgendamentoService, HistoricoAgendamentoService>();
            services.AddScoped<ISolicitacaoExameService, SolicitacaoExameService>();
            services.AddScoped<IAgendamentoService, AgendamentoService>();
            services.AddScoped<IBlobStorageService, BlobStorageService>();

            return services;
        }
    }
}
