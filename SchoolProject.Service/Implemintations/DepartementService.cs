using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.IRepositories;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class DepartementService : IDeparetementService
    {
        private readonly IDepartementRepository _deparetementrepository;
        public DepartementService(IDepartementRepository deparetementrepository)
        {
            _deparetementrepository = deparetementrepository;
        }

        public async Task<Departement> GetDepartementById(int id)
        {
            var departement = await _deparetementrepository.GetTableNoTracking().Where(x => x.DID.Equals(id))
                                   .Include(x => x.DepartementSubjects).ThenInclude(x => x.Subject)
                                   .Include(x => x.Instructors)
                                   .Include(x => x.Instructor).FirstOrDefaultAsync();
            return departement;
        }
    }
}
