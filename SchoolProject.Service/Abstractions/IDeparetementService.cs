using SchoolProject.Data.Entities;

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

    }
}
