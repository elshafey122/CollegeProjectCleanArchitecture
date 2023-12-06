using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Service.Abstractions
{
    public interface IDeparetementService
    {
        public Task<Departement> GetDepartementById(int id);
        public IQueryable<Departement> GetDepartementList();
        public Task<string> AddDepartement(Departement departement);
        public Task<string> EditDepartement(Departement departement);
        public Task<string> DeleteDepartement(int id);
        public Task<bool> IsDepartementExist(int? id);
        public Task<List<ViewDepartStudentCount>> viewDepartStudentCounts();
        public Task<IReadOnlyList<DepartstudentContProcedeur>> GetdepartstudentContProcedeurs(DepartstudentContProcedeurParams param);
    }
}
