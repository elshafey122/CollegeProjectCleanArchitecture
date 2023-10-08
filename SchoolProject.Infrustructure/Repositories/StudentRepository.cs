using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.IRepositories;

namespace SchoolProject.Infrustructure.Repositories
{
    public class StudentRepository : GenericRepositories<Student>, IStudentRepository
    {
        private readonly DbSet<Student> _students;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _students = context.Set<Student>();
        }
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _students.Include(x => x.Departement).ToListAsync();
        }
    }
}
