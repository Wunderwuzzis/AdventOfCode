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

    private IEnumerable<char[]> _matchData;

    private Dictionary<char, char> _rpsChars = new() { ['A'] = 'X', ['B'] = 'Y', ['C'] = 'Z' };
    private Dictionary<char, char> _rules = new() { ['X'] = 'Z', ['Y'] = 'X', ['Z'] = 'Y' };
    private Dictionary<char, Result> _results = new() { ['X'] = Result.Defeat, ['Y'] = Result.Draw, ['Z'] = Result.Victory };

    private Dictionary<char, int> _shapeScores = new() { ['X'] = 1, ['Y'] = 2, ['Z'] = 3 };
    private Dictionary<Result, int> _resultScores = new() { [Result.Defeat] = 0, [Result.Draw] = 3, [Result.Victory] = 6 };

    public Day2(string title, int target1 = default, int target2 = default) : base(2, title, target1, target2)
    {
        _matchData = Data.Select(line => line.Split(' ').Select(GetRpsChar).ToArray());
    }

    private char GetRpsChar(string input)
    {
        var c = char.Parse(input);
        return _rpsChars.TryGetValue(c, out var rps) ? rps : c;
    }

    protected override int Part1()
    {
        return CalculateGameScore1().Sum();
    }

    private IEnumerable<int> CalculateGameScore1()
    {
        foreach (var (enemy, own) in _matchData)
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

    protected override int Part2()
    {
        return CalculateGameScore2().Sum();
    }

    private IEnumerable<int> CalculateGameScore2()
    {
        foreach (var (enemy, resultKey) in _matchData)
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