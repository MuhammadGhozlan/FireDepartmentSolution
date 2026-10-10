using FireTime.Interfaces.RepoInterfaces;
using FireTime.Models;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Repositories;

public sealed class EfCrudRepository<TEntity, TKey>(IisFireTimeContext context)
    : ICrudRepository<TEntity, TKey> where TEntity : class
{
    private readonly DbSet<TEntity> _set = context.Set<TEntity>();
    private bool HasProperty(string name) =>
        context.Model.FindEntityType(typeof(TEntity))?.FindProperty(name) is not null;

    public bool SupportsSoftDelete => HasProperty("DeletedInd");

    public async Task<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken) =>
        await _set.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<TEntity>> ListAsync(int offset, int limit, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = _set.AsNoTracking();
        if (SupportsSoftDelete)
            query = query.Where(entity => !EF.Property<bool>(entity, "DeletedInd"));
        var keyName = context.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.Single().Name
            ?? throw new InvalidOperationException("A single-column primary key is required.");
        return await query.OrderBy(entity => EF.Property<TKey>(entity, keyName))
            .Skip(offset).Take(limit).ToListAsync(cancellationToken);
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken) =>
        _set.AddAsync(entity, cancellationToken).AsTask();

    public Task SaveAsync(CancellationToken cancellationToken) =>
        context.SaveChangesAsync(cancellationToken);

    public bool IsDeleted(TEntity entity) =>
        SupportsSoftDelete && (bool)context.Entry(entity).Property("DeletedInd").CurrentValue!;

    public void MarkDeleted(TEntity entity) =>
        context.Entry(entity).Property("DeletedInd").CurrentValue = true;

    public void StampCreated(TEntity entity, string actor, DateTime timestamp)
    {
        if (HasProperty("CreatedBy"))
            context.Entry(entity).Property("CreatedBy").CurrentValue = actor;
        if (HasProperty("CreatedDate"))
            context.Entry(entity).Property("CreatedDate").CurrentValue = timestamp;
        StampUpdated(entity, actor, timestamp);
    }

    public void StampUpdated(TEntity entity, string actor, DateTime timestamp)
    {
        if (HasProperty("LastUpdateBy"))
            context.Entry(entity).Property("LastUpdateBy").CurrentValue = actor;
        if (HasProperty("LastUpdateDate"))
            context.Entry(entity).Property("LastUpdateDate").CurrentValue = timestamp;
    }
}
