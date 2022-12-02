using AoC.Utils;

namespace AoC;

public class Day2 : Day<int>
{
    public Day2(string title, int? target1 = null, int? target2 = null) : base(2, title, target1, target2) { }

    private Dictionary<char, Result> _results = new() { ['X'] = Result.Defeat, ['Y'] = Result.Draw, ['Z'] = Result.Victory };
    private Dictionary<char, char> _draws = new() { ['X'] = 'A', ['Y'] = 'B', ['Z'] = 'C' };
    private Dictionary<char, char> _rules = new() { ['X'] = 'C', ['Y'] = 'A', ['Z'] = 'B' };
    private Dictionary<char, char> _defeat = new() { ['X'] = 'B', ['Y'] = 'C', ['Z'] = 'A' };
    private Dictionary<char, int> _scores = new() { ['X'] = 1, ['Y'] = 2, ['Z'] = 3 };

    private enum Result
    {
        Defeat = 0,
        Draw = 3,
        Victory = 6
    }

    protected override int Part1()
    {
        return CalculateGameScore1().Sum();
    }

    private IEnumerable<int> CalculateGameScore1()
    {
        foreach (var line in Data)
        {
            var (enemy, own) = line.Split(' ').Select(char.Parse).ToArray();
            if (_draws[own] == enemy)
                yield return CalculateScore(own, Result.Draw);
            else if (_rules[own] == enemy)
                yield return CalculateScore(own, Result.Victory);
            else
                yield return CalculateScore(own, Result.Defeat);
        }
    }

    private int CalculateScore(char own, Result draw)
    {
        return _scores[own] + (int) draw;
    }

    protected override int Part2()
    {
        return CalculateGameScore2().Sum();
    }

    private IEnumerable<int> CalculateGameScore2()
    {
        foreach (var line in Data)
        {
            var (enemy, resultKey) = line.Split(' ').Select(char.Parse).ToArray();
            var result = _results[resultKey];
            var own = GetRequired(enemy, result);
            yield return CalculateScore(own, result);
        }
    }

    private char GetRequired(char enemy, Result result)
    {
        return result switch
        {
            Result.Defeat => _defeat.First(x => x.Value == enemy).Key,
            Result.Draw => _draws.First(x => x.Value == enemy).Key,
            Result.Victory => _rules.First(x => x.Value == enemy).Key,
            _ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
        };
    }
}