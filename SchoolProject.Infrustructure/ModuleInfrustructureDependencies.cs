using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.IRepositories;
using SchoolProject.Infrustructure.IRepositories.IFunctions;
using SchoolProject.Infrustructure.IRepositories.IProcedures;
using SchoolProject.Infrustructure.IRepositories.IViews;
using SchoolProject.Infrustructure.Repositories;
using SchoolProject.Infrustructure.Repositories.Functions;
using SchoolProject.Infrustructure.Repositories.Procedures;
using SchoolProject.Infrustructure.Repositories.Views;

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
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositories<>), typeof(GenericRepositories<>));

            services.AddTransient<IViewRepository<ViewDepartStudentCount>, ViewDeptRepository>();
            services.AddTransient<IDepartstudentContProcedeurRepository, DepartstudentContProcedeurRepository>();

            //funcs
            services.AddTransient<IInstructorFunctionRepository, InstructorFunctionRepository>();



            return services;
        }
    }
}
