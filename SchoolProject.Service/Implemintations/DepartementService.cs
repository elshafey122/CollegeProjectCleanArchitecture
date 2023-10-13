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

        public async Task<string> AddDepartement(Departement departement)
        {
            var result = await _deparetementrepository.GetTableNoTracking().Where(x => x.DNameAr.Equals(departement.DNameAr) || x.DNameEn.Equals(departement.DNameEn)).FirstOrDefaultAsync();
            if (result != null)
            {
                return "Exist";
            }
            await _deparetementrepository.AddAsync(departement);
            return "Success";
        }

        public async Task<string> DeleteDepartement(int id)
        {
            var result = await _deparetementrepository.GetByIdAsync(id);
            if (result != null)
            {
                var transaction = _deparetementrepository.BeginTransaction();
                try
                {
                    await _deparetementrepository.DeleteAsync(result);
                    transaction.Commit();
                    return "Success";
                }
                catch
                {
                    transaction.Rollback();
                    return "Failure";
                }
            }
            return "NotFound";
        }
        public async Task<string> EditDepartement(Departement departement)
        {
            await _deparetementrepository.UpdateAsync(departement);
            return "Success";
        }

        public async Task<Departement> GetDepartementById(int id)
        {
            var departement = await _deparetementrepository.GetTableNoTracking().Where(x => x.DID.Equals(id))
                                   .Include(x => x.DepartementSubjects).ThenInclude(x => x.Subject)
                                   .Include(x => x.Instructors)
                                   .Include(x => x.Instructor).FirstOrDefaultAsync();
            return departement;
        }

        public IQueryable<Departement> GetDepartementList()
        {
            var departements = _deparetementrepository.GetTableNoTracking().Include(x => x.Instructor).AsQueryable();
            return departements;
        }

        public async Task<bool> IsDepartementExist(int? id)
        {
            var res = await _deparetementrepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.DID == id);
            if (res == null)
            {
                return false;
            }
            return true;
        }
    }
}
