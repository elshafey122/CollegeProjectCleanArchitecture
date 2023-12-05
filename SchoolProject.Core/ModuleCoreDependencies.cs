using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Core.Filters;
using System.Reflection;
namespace SchoolProject.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            // configuration for mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // configuration for automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // adding validation 
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // add filter to handle cases in authorization
            services.AddTransient<AuthFilter>();

            return services;
        }
    }
}
