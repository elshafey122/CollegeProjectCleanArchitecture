using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.IRepositories;

namespace SchoolProject.Infrustructure.Repositories
{
    public class DepartementRepository : GenericRepositories<Departement>, IDepartementRepository
    {
        public DbSet<Departement> departements;
        public DepartementRepository(ApplicationDbContext context) : base(context)
        {
            departements = context.Set<Departement>();
        }
    }
}
