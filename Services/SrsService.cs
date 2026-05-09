using jappo.Models;

namespace jappo.Services;

public sealed class SrsService(IStorageService storage, ICharacterRepository repo) : ISrsService
{
    private const string KeyPrefix = "jappo:srs:";

    public async Task<SrsCard> GetOrCreateCardAsync(string characterId)
    {
        var card = await storage.GetAsync<SrsCard>($"{KeyPrefix}{characterId}");
        if (card is not null) return card;

        card = new SrsCard { CharacterId = characterId };
        await storage.SetAsync($"{KeyPrefix}{characterId}", card);
        return card;
    }

    public async Task ApplyReviewAsync(SrsCard card, SrsQuality quality)
    {
        var q = (int)quality;

        // SM-2: update ease factor
        card.EaseFactor += 0.1 - (5 - q) * (0.08 + (5 - q) * 0.02);
        if (card.EaseFactor < 1.3) card.EaseFactor = 1.3;

        if (q < 3)
        {
            card.Repetitions = 0;
            card.IntervalDays = 1;
        }
        else
        {
            card.IntervalDays = card.Repetitions switch
            {
                0 => 1,
                1 => 6,
                _ => (int)Math.Round(card.IntervalDays * card.EaseFactor)
            };
            card.Repetitions++;
        }

        card.LastReview = DateTimeOffset.UtcNow;
        card.NextReview = DateTimeOffset.UtcNow.AddDays(card.IntervalDays);

        await storage.SetAsync($"{KeyPrefix}{card.CharacterId}", card);
    }

    public async Task<List<SrsCard>> GetDueCardsAsync(CharacterSet[]? sets = null, int limit = 20)
    {
        var characters = sets is { Length: > 0 }
            ? repo.GetAll().Where(c => sets.Contains(c.Set)).ToList()
            : repo.GetAll().ToList();

        var cards = new List<SrsCard>();
        foreach (var ch in characters)
        {
            var card = await GetOrCreateCardAsync(ch.Id);
            if (card.IsDue) cards.Add(card);
        }

        return cards
            .OrderBy(c => c.NextReview)
            .Take(limit)
            .ToList();
    }

    public async Task ResetProgressAsync(CharacterSet? set = null)
    {
        var characters = set is null
            ? repo.GetAll()
            : repo.GetAll().Where(c => c.Set == set.Value);

        foreach (var ch in characters)
            await storage.RemoveAsync($"{KeyPrefix}{ch.Id}");
    }

    public async Task<Dictionary<CharacterSet, (int Total, int Due, int Learned)>> GetStatsAsync()
    {
        var result = new Dictionary<CharacterSet, (int, int, int)>();

        foreach (var set in Enum.GetValues<CharacterSet>())
        {
            var chars = repo.GetAll().Where(c => c.Set == set).ToList();
            int due = 0, learned = 0;

            foreach (var ch in chars)
            {
                var card = await GetOrCreateCardAsync(ch.Id);
                if (card.IsDue) due++;
                if (card.Repetitions > 0) learned++;
            }

            result[set] = (chars.Count, due, learned);
        }

        return result;
    }
}
