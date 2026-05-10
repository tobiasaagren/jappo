using System.Text;

namespace jappo.Utilities;

public static class RomajiConverter
{
    private static readonly Dictionary<string, string> Map = new()
    {
        // Digraphs (checked before singles)
        {"きゃ","kya"},{"きゅ","kyu"},{"きょ","kyo"},
        {"しゃ","sha"},{"しゅ","shu"},{"しょ","sho"},
        {"ちゃ","cha"},{"ちゅ","chu"},{"ちょ","cho"},
        {"にゃ","nya"},{"にゅ","nyu"},{"にょ","nyo"},
        {"ひゃ","hya"},{"ひゅ","hyu"},{"ひょ","hyo"},
        {"みゃ","mya"},{"みゅ","myu"},{"みょ","myo"},
        {"りゃ","rya"},{"りゅ","ryu"},{"りょ","ryo"},
        {"ぎゃ","gya"},{"ぎゅ","gyu"},{"ぎょ","gyo"},
        {"じゃ","ja"}, {"じゅ","ju"}, {"じょ","jo"},
        {"びゃ","bya"},{"びゅ","byu"},{"びょ","byo"},
        {"ぴゃ","pya"},{"ぴゅ","pyu"},{"ぴょ","pyo"},
        // Singles
        {"あ","a"},  {"い","i"},  {"う","u"},  {"え","e"},  {"お","o"},
        {"か","ka"}, {"き","ki"}, {"く","ku"}, {"け","ke"}, {"こ","ko"},
        {"さ","sa"}, {"し","shi"},{"す","su"}, {"せ","se"}, {"そ","so"},
        {"た","ta"}, {"ち","chi"},{"つ","tsu"},{"て","te"}, {"と","to"},
        {"な","na"}, {"に","ni"}, {"ぬ","nu"}, {"ね","ne"}, {"の","no"},
        {"は","ha"}, {"ひ","hi"}, {"ふ","fu"}, {"へ","he"}, {"ほ","ho"},
        {"ま","ma"}, {"み","mi"}, {"む","mu"}, {"め","me"}, {"も","mo"},
        {"や","ya"}, {"ゆ","yu"}, {"よ","yo"},
        {"ら","ra"}, {"り","ri"}, {"る","ru"}, {"れ","re"}, {"ろ","ro"},
        {"わ","wa"}, {"を","wo"}, {"ん","n"},
        {"が","ga"}, {"ぎ","gi"}, {"ぐ","gu"}, {"げ","ge"}, {"ご","go"},
        {"ざ","za"}, {"じ","ji"}, {"ず","zu"}, {"ぜ","ze"}, {"ぞ","zo"},
        {"だ","da"}, {"ぢ","ji"}, {"づ","zu"}, {"で","de"}, {"ど","do"},
        {"ば","ba"}, {"び","bi"}, {"ぶ","bu"}, {"べ","be"}, {"ぼ","bo"},
        {"ぱ","pa"}, {"ぴ","pi"}, {"ぷ","pu"}, {"ぺ","pe"}, {"ぽ","po"},
    };

    public static string Convert(string hiragana)
    {
        if (string.IsNullOrEmpty(hiragana)) return string.Empty;

        var sb = new StringBuilder();
        var i = 0;
        while (i < hiragana.Length)
        {
            if (hiragana[i] == 'っ')
            {
                i++;
                // Double the initial consonant of the next syllable
                string? peek = null;
                if (i + 1 < hiragana.Length) Map.TryGetValue(hiragana.Substring(i, 2), out peek);
                if (peek is null && i < hiragana.Length) Map.TryGetValue(hiragana[i].ToString(), out peek);
                if (peek?.Length > 0 && peek[0] is not ('a' or 'i' or 'u' or 'e' or 'o'))
                    sb.Append(peek[0]);
                continue;
            }

            if (i + 1 < hiragana.Length && Map.TryGetValue(hiragana.Substring(i, 2), out var digraph))
            {
                sb.Append(digraph);
                i += 2;
                continue;
            }

            sb.Append(Map.TryGetValue(hiragana[i].ToString(), out var single) ? single : hiragana[i]);
            i++;
        }
        return sb.ToString();
    }
}
