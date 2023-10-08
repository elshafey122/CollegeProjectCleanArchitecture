using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstractions
{
    public interface IDeparetementService
    {
        public Task<Departement> GetDepartementById(int id);
    }
}
