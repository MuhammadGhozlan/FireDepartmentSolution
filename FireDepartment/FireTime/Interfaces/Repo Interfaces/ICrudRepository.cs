using Microsoft.EntityFrameworkCore;
using FireTime.Models;

namespace FireTime.Interfaces.RepoInterfaces;

public interface ICrudRepository<TEntity, TKey> where TEntity : class
{
    Task<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TEntity>> ListAsync(int offset, int limit, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
    bool IsDeleted(TEntity entity);
    bool SupportsSoftDelete { get; }
    void MarkDeleted(TEntity entity);
    void StampCreated(TEntity entity, string actor, DateTime timestamp);
    void StampUpdated(TEntity entity, string actor, DateTime timestamp);
}
