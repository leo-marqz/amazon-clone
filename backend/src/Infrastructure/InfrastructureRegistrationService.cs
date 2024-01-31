
using Ecommerce.Application.Abstracts;
using Ecommerce.Application.Models.Token;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureRegistrationService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration){
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(RepositoryBase<>));
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings")); //es para tener acceso a los datos jwt
            return services;
        }
    }
}