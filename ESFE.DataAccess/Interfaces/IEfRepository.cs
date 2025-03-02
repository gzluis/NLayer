using Ardalis.Specification;

namespace ESFE.DataAccess.Interfaces
{
    public interface IEfRepository<T> : IRepositoryBase<T> where T : class
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
