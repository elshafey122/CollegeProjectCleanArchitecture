using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstractions;
using SchoolProject.Service.Implemintations;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDeparetementService, DepartementService>();
            return services;
        }
    }
}