namespace FireTime.Interfaces.ServiceInterfaces;

public interface ICrudService<TRequest, TResponse, TKey>
{
    Task<TResponse?> GetAsync(TKey id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TResponse>> ListAsync(int offset, int limit, CancellationToken cancellationToken);
    Task<TResponse> CreateAsync(TRequest request, CancellationToken cancellationToken);
    Task<TResponse?> UpdateAsync(TKey id, TRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken);
    bool SupportsDelete { get; }
}
