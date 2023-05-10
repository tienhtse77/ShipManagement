using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.DbInitializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ShipManagementDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(
                    options =>
                        options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version()), option =>
                    {
                        option.UseNetTopologySuite();
                    }));
            }

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
