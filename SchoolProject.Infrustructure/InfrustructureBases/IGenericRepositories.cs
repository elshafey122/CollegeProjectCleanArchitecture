using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolProject.Infrustructure.InfrustructureBases
{
    public interface IGenericRepositories<T> where T : class
    {
        Task DeleteRangeAsync(ICollection<T> entities);
        Task<T> GetByIdAsync(int id);
        IDbContextTransaction BeginTransaction();
        void commit();
        void RollBack();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task commitAsync();
        Task RollBackAsync();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);
    }
}
