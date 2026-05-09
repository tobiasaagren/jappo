using System.Net.Http.Json;
using System.Text.Json;
using jappo.Models;

namespace jappo.Services;

public sealed class CharacterRepository(HttpClient http) : ICharacterRepository
{
    private readonly Dictionary<string, Character> _byId = [];
    private bool _loaded;

    public async Task InitializeAsync()
    {
        if (_loaded) return;

        var opts = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        foreach (var file in new[] { "hiragana", "katakana", "kanji-n5" })
        {
            var data = await http.GetFromJsonAsync<CharacterData>($"data/{file}.json", opts);
            if (data?.Characters is null) continue;
            foreach (var ch in data.Characters)
                _byId[ch.Id] = ch;
        }

        _loaded = true;
    }

    public IReadOnlyList<Character> GetAll() => [.. _byId.Values];

    public IReadOnlyList<Character> GetBySet(CharacterSet set) =>
        _byId.Values.Where(c => c.Set == set).ToList();

    public Character? GetById(string id) =>
        _byId.TryGetValue(id, out var ch) ? ch : null;

    private sealed class CharacterData
    {
        public List<Character>? Characters { get; set; }
    }
}
