using jappo.Models;

namespace jappo.Services;

public interface ICharacterRepository
{
    IReadOnlyList<Character> GetAll();
    IReadOnlyList<Character> GetBySet(CharacterSet set);
    Character? GetById(string id);
    Task InitializeAsync();
}
