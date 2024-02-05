using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.Interfaces;
using Projeto.Application.Mappings;
using Projeto.Application.Services;
using Projeto.Domain.Interfaces;
using Projeto.Infra.Dados.ClienteRepositorio;

namespace Projeto.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IClienteService, ClienteService>();

            services.AddSingleton(configuration);
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
