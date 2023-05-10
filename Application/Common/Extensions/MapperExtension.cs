using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper.EquivalencyExpression;
using Application.Common.Models.Mapping;

namespace Watt.Backend.Api.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection ConfigProfileMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(options => {
                options.AddCollectionMappers();
                options.AddProfile<MappingProfile>();
            }, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}