using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.IRepositories;

namespace SchoolProject.Infrustructure.Repositories
{
    public class InstructorRepository : GenericRepositories<Instructor>, IInstructorRepository
    {
        public DbSet<Instructor> instructors;
        public InstructorRepository(ApplicationDbContext context) : base(context)
        {
            instructors = context.Set<Instructor>();
        }
    }
}
