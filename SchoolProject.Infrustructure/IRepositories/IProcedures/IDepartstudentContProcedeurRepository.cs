using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Infrustructure.IRepositories.IProcedures
{
    public interface IDepartstudentContProcedeurRepository
    {
        public Task<IReadOnlyList<DepartstudentContProcedeur>> GetdepartstudentContProcedeurs(DepartstudentContProcedeurParams param);
    }
}
