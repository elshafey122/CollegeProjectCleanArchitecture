using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.IRepositories;

namespace SchoolProject.Infrustructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositories<UserRefreshToken>, IRefreshTokenRepository
    {
        public DbSet<UserRefreshToken> _userRefreshToken;
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
            _userRefreshToken = context.Set<UserRefreshToken>();
        }
    }
}
