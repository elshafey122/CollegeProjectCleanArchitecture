using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.IRepositories.IProcedures;
using StoredProcedureEFCore;

namespace SchoolProject.Infrustructure.Repositories.Procedures
{
    public class DepartstudentContProcedeurRepository : IDepartstudentContProcedeurRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartstudentContProcedeurRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<DepartstudentContProcedeur>> GetdepartstudentContProcedeurs(DepartstudentContProcedeurParams param)
        {
            var rows = new List<DepartstudentContProcedeur>();
            var result = _context.LoadStoredProc(nameof(DepartstudentContProcedeur))
                .AddParam(nameof(DepartstudentContProcedeurParams.DId), param.DId)
                .ExecAsync(async r => rows = await r.ToListAsync<DepartstudentContProcedeur>());
            return rows;
        }
    }
}
