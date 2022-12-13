using AoC.Utils;

namespace AoC;

public class Day2 : Day<int>
{
    private enum Result
    {
        Defeat,
        Draw,
        Victory
    }

    private Dictionary<char, char> _rpsChars = new() { ['A'] = 'X', ['B'] = 'Y', ['C'] = 'Z' };
    private Dictionary<char, char> _rules = new() { ['X'] = 'Z', ['Y'] = 'X', ['Z'] = 'Y' };
    private Dictionary<char, Result> _results = new() { ['X'] = Result.Defeat, ['Y'] = Result.Draw, ['Z'] = Result.Victory };

    private Dictionary<char, int> _shapeScores = new() { ['X'] = 1, ['Y'] = 2, ['Z'] = 3 };
    private Dictionary<Result, int> _resultScores = new() { [Result.Defeat] = 0, [Result.Draw] = 3, [Result.Victory] = 6 };

    public Day2(string title, int target1 = default, int target2 = default) : base(2, title, target1, target2) { }


    protected override void ExecuteParts(out int part1, out int part2)
    {
        var matchData = Data.Select(line => line.Split(' ').Select(GetRpsChar).ToArray()).ToArray();
        part1 = CalculateGameScore1(matchData).Sum();
        part2 = CalculateGameScore2(matchData).Sum();
    }

    private char GetRpsChar(string input)
    {
        var c = char.Parse(input);
        return _rpsChars.TryGetValue(c, out var rps) ? rps : c;
    }

    private IEnumerable<int> CalculateGameScore1(IEnumerable<char[]> matchData)
    {
        foreach (var (enemy, own) in matchData)
        {
            if (own == enemy)
                yield return CalculateScore(own, Result.Draw);
            else if (_rules[own] == enemy)
                yield return CalculateScore(own, Result.Victory);
            else
                yield return CalculateScore(own, Result.Defeat);
        }
    }

    private int CalculateScore(char own, Result result)
    {
        return _shapeScores[own] + _resultScores[result];
    }

    private IEnumerable<int> CalculateGameScore2(IEnumerable<char[]> matchData)
    {
        foreach (var (enemy, resultKey) in matchData)
        {
            var result = _results[resultKey];
            var own = GetRequired(enemy, result);
            yield return CalculateScore(own, result);
        }
    }

    private char GetRequired(char enemy, Result result)
    {
        return result switch
        {
            Result.Defeat => _rules[enemy],
            Result.Draw => enemy,
            Result.Victory => _rules.First(x => x.Value == enemy).Key,
            _ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
        };
    }
}