namespace jappo.Models;

public enum CharacterSet { Hiragana, Katakana, Kanji }

public enum StudyMode { Flashcard, MultipleChoice, Typing, StrokeOrder }

public enum SrsQuality
{
    Blackout = 0,
    Incorrect = 1,
    Hard = 2,
    Good = 3,
    Easy = 4,
    Perfect = 5
}
