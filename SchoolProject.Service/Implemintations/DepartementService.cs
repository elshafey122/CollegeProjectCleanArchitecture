using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrustructure.IRepositories;
using SchoolProject.Infrustructure.IRepositories.IProcedures;
using SchoolProject.Infrustructure.IRepositories.IViews;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class DepartementService : IDeparetementService
    {
        private readonly IDepartementRepository _deparetementrepository;
        private readonly IViewRepository<ViewDepartStudentCount> _viewDeptRepository;
        private readonly IDepartstudentContProcedeurRepository _departstudentContProcedeurRepository;

        public DepartementService(IDepartementRepository deparetementrepository,
            IViewRepository<ViewDepartStudentCount> viewDeptRepository,
            IDepartstudentContProcedeurRepository departstudentContProcedeurRepository)
        {
            _deparetementrepository = deparetementrepository;
            _viewDeptRepository = viewDeptRepository;
            _departstudentContProcedeurRepository = departstudentContProcedeurRepository;
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

        public async Task<IReadOnlyList<DepartstudentContProcedeur>> GetdepartstudentContProcedeurs(DepartstudentContProcedeurParams param)
        {
            return await _departstudentContProcedeurRepository.GetdepartstudentContProcedeurs(param);
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

        public async Task<List<ViewDepartStudentCount>> viewDepartStudentCounts()
        {
            return await _viewDeptRepository.GetTableNoTracking().ToListAsync();
        }
    }
}
