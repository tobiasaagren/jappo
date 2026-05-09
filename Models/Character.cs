namespace jappo.Models;

public sealed class Character
{
    public required string Id { get; init; }
    public required CharacterSet Set { get; init; }
    public required string Glyph { get; init; }
    public required string[] Readings { get; init; }
    public string? Meaning { get; init; }
    public string? MnemonicHint { get; init; }
    public string Unicode { get; init; } = string.Empty;
    public bool HasStrokeSvg { get; init; }
    public int StrokeCount { get; init; }
    public ExampleWord[] ExampleWords { get; init; } = [];
}
