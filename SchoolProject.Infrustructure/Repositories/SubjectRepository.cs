using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.IRepositories;

namespace SchoolProject.Infrustructure.Repositories
{
    public class SubjectRepository : GenericRepositories<Subject>, ISubjectRepository
    {
        public DbSet<Subject> subjects;
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
            subjects = context.Set<Subject>();
        }
    }
}
