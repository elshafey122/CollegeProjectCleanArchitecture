using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.IRepositories;
using SchoolProject.Infrustructure.Repositories;

namespace SchoolProject.Infrustructure
{
    public static class ModuleInfrustructureDependencies
    {
        public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartementRepository, DepartementRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient(typeof(IGenericRepositories<>), typeof(GenericRepositories<>));
            return services;
        }
    }
}
