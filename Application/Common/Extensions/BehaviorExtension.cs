using Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Watt.Backend.Manager.PipelineBehavior;

namespace Application.Common.Extensions
{
    public static class BehaviorExtension
    {
        public static IServiceCollection ConfigPipelineBehavior(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            return services;
        }
    }
}