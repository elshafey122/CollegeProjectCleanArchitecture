using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.IRepositories
{
    public interface IStudentRepository : IGenericRepositories<Student>
    {
        public Task<List<Student>> GetStudentsListAsync();
    }
}
