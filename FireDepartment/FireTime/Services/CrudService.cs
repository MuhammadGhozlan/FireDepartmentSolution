using FireTime.Interfaces.RepoInterfaces;
using FireTime.Interfaces.ServiceInterfaces;

namespace FireTime.Services;

public sealed class CrudService<TEntity, TRequest, TResponse, TKey>(
    ICrudRepository<TEntity, TKey> repository,
    IHttpContextAccessor httpContextAccessor,
    Func<TRequest, TEntity> create,
    Action<TEntity, TRequest> update,
    Func<TEntity, TResponse> map)
    : ICrudService<TRequest, TResponse, TKey> where TEntity : class
{
    private string Actor => httpContextAccessor.HttpContext?.User.Identity?.Name ?? "local-development";
    public bool SupportsDelete => repository.SupportsSoftDelete;

    public async Task<TResponse?> GetAsync(TKey id, CancellationToken cancellationToken)
    {
        var entity = await repository.FindAsync(id, cancellationToken);
        return entity is null || repository.IsDeleted(entity) ? default : map(entity);
    }

    public async Task<IReadOnlyList<TResponse>> ListAsync(int offset, int limit, CancellationToken cancellationToken)
    {
        var entities = await repository.ListAsync(offset, limit, cancellationToken);
        return entities.Select(map).ToArray();
    }

    public async Task<TResponse> CreateAsync(TRequest request, CancellationToken cancellationToken)
    {
        var entity = create(request);
        repository.StampCreated(entity, Actor, DateTime.UtcNow);
        await repository.AddAsync(entity, cancellationToken);
        await repository.SaveAsync(cancellationToken);
        return map(entity);
    }

    public async Task<TResponse?> UpdateAsync(TKey id, TRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.FindAsync(id, cancellationToken);
        if (entity is null || repository.IsDeleted(entity))
            return default;
        update(entity, request);
        repository.StampUpdated(entity, Actor, DateTime.UtcNow);
        await repository.SaveAsync(cancellationToken);
        return map(entity);
    }

    public async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken)
    {
        if (!SupportsDelete)
            return false;
        var entity = await repository.FindAsync(id, cancellationToken);
        if (entity is null || repository.IsDeleted(entity))
            return false;
        repository.MarkDeleted(entity);
        repository.StampUpdated(entity, Actor, DateTime.UtcNow);
        await repository.SaveAsync(cancellationToken);
        return true;
    }
}
