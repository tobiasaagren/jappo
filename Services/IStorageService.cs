namespace jappo.Services;

public interface IStorageService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value);
    Task RemoveAsync(string key);
    Task<List<string>> GetKeysWithPrefixAsync(string prefix);
}
