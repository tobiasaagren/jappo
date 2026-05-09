namespace jappo.Models;

public sealed class SrsCard
{
    public required string CharacterId { get; init; }
    public int Repetitions { get; set; }
    public double EaseFactor { get; set; } = 2.5;
    public int IntervalDays { get; set; } = 1;
    public DateTimeOffset NextReview { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? LastReview { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public bool IsDue => DateTimeOffset.UtcNow >= NextReview;
}
