using jappo.Models;

namespace jappo.Services;

public interface ISrsService
{
    Task<List<SrsCard>> GetDueCardsAsync(CharacterSet[]? sets = null, int limit = 20);
    Task ApplyReviewAsync(SrsCard card, SrsQuality quality);
    Task<SrsCard> GetOrCreateCardAsync(string characterId);
    Task ResetProgressAsync(CharacterSet? set = null);
    Task<Dictionary<CharacterSet, (int Total, int Due, int Learned)>> GetStatsAsync();
}
