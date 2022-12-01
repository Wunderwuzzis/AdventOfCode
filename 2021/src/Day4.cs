using System;
using System.Collections.Generic;
using System.Text;

namespace AoC;

public class Day4 : Day<int>
{
    private int[] draws;
    private List<Board> boards;
    public Day4(string dataPath) : base(dataPath)
    {
        draws = data[0].ExtractAllInt();
        boards = ExtractBoards(data, start: 2, Board.Size, margin: 1);
    }

    protected override int Part1()
    {
        foreach(var draw in draws)
            foreach (var b in boards)
                if (MatchAndCheck(b, draw))
                    return draw * b.SumUnflagged();
        return 0;
    }

    protected override int Part2()
    {
        foreach(var draw in draws)
        {
            for (int i = boards.Count - 1; i >= 0; i--)
            {
                if (MatchAndCheck(boards[i], draw))
                {
                    var lastSum = boards[i].SumUnflagged();
                    boards.RemoveAt(i);
                    if (boards.Count == 0)
                        return draw * lastSum;
                }
            }
        }
        return 0;
    }

    public static List<Board> ExtractBoards(string[] array, int start, int size, int margin)
    {
        var result = new List<Board>();
        var tempBoard = new string[size];
        int i = start;
        while (i < array.Length)
        {
            var board = new int[size][];
            Array.Copy(array, i, tempBoard, 0, size);
            for (int index = 0; index < size; index++)
            {
                var numbers = tempBoard[index].ExtractAllInt(Board.Size);
                board[index] = new int[size];
                numbers.CopyTo(board[index], 0);
            }
            result.Add(new Board(board));
            i = i + size + margin;
        }
        return result;
    }

    private static bool MatchAndCheck(Board board, int number)
    {
        for (int y = 0; y < Board.Size; y++)
            for (int x = 0; x < Board.Size; x++)
                if (board.Match(x, y, number))
                    return board.CheckX(x) || board.CheckY(y);
        return false;
    }
}

public class Board
{
    public const int Size = 5;
    private const int Flag = -1;
    private int[][] array;

    public Board(int[][] content)
    {
        array = content;
    }

    public bool Match(int x, int y, int number)
    {
        if (array[y][x] == number)
        {
            array[y][x] = Flag;
            return true;
        }
        return false;
    }

    public bool CheckX(int x)
    {
        for (int i = 0; i < Size; i++)
            if (array[i][x] != Flag)
                return false;
        return true;
    }

    public bool CheckY(int y)
    {
        for (int i = 0; i < Size; i++)
            if (array[y][i] != Flag)
                return false;
        return true;
    }

    public int SumUnflagged()
    {
        int sum = 0;
        for (int y = 0; y < Size; y++)
            for (int x = 0; x < Size; x++)
                if (array[y][x] != Flag)
                    sum += array[y][x];
        return sum;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var number in array)
        {
            sb.AppendJoin(" ", number);
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
