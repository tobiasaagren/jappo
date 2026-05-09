using Microsoft.JSInterop;
using System.Text.Json;

namespace jappo.Services;

public sealed class LocalStorageService(IJSRuntime js) : IStorageService
{
    private static readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await js.InvokeAsync<string?>("jappoStorage.get", key);
        if (json is null) return default;
        return JsonSerializer.Deserialize<T>(json, _opts);
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await js.InvokeVoidAsync("jappoStorage.set", key, json);
    }

    public async Task RemoveAsync(string key)
    {
        await js.InvokeVoidAsync("jappoStorage.remove", key);
    }

    public async Task<List<string>> GetKeysWithPrefixAsync(string prefix)
    {
        return await js.InvokeAsync<List<string>>("jappoStorage.keysWithPrefix", prefix);
    }
}
