namespace AoC;

public class Day13 : Day<int>
{
    public Day13(string title, int target1 = default, int target2 = default) : base(13, title, target1, target2) { }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        var packets = Data.Chunk(3).Select(x => (x[0], x[1])).ToArray();

        var validIndices = new List<int>();
        var packetIndex = 0;

        foreach (var (left, right) in packets)
        {
            packetIndex++;
            if (CheckAscendingPacket(left, right))
                validIndices.Add(packetIndex);
        }

        part1 = validIndices.Sum();
        part2 = 0;
    }

    private static bool CheckAscendingPacket(string inputL, string inputR)
    {
        var indexL = 0;
        var indexR = 0;
        var bufferL = 0;
        var bufferR = 0;

        while (indexL < inputL.Length)
        {
            if (indexR >= inputR.Length)
                return false;

            var cL = inputL[++indexL];
            var cR = inputR[++indexR];

            var leftIsNumber = TryGetNextInteger(out var valueL, ref indexL, inputL);
            var rightIsNumber = TryGetNextInteger(out var valueR, ref indexR, inputR);

            if (leftIsNumber && rightIsNumber)
            {
                if (valueL < valueR) return true;
                if (valueL > valueR) return false;
                if (Release(ref bufferL, cR, ref indexR)) return true;
                if (Release(ref bufferR, cL, ref indexL)) return false;
            }
            else if (cL == ']' && !Close(cR, ref indexR)) return true;
            else if (cR == ']' && !Close(cL, ref indexL)) return false;
            else if (leftIsNumber) Keep(ref bufferL, ref indexL);
            else if (rightIsNumber) Keep(ref bufferR, ref indexR);

            bool Close(char c, ref int index)
            {
                if (c == ']')
                    index++;
                else
                    return false;
                return true;
            }

            void Keep(ref int buffer, ref int index)
            {
                buffer++;
                index--;
            }

            bool Release(ref int buffer, char otherChar, ref int otherIndex)
            {
                while (buffer > 0)
                {
                    buffer--;
                    if (!Close(otherChar, ref otherIndex)) return true;
                }

                return false;
            }
        }

        return true;
    }

    private static bool TryGetNextInteger(out int result, ref int index, string text)
    {
        result = -1;

        if (!char.IsNumber(text[index]))
            return false;

        var startIndex = index;
        while (char.IsNumber(text[startIndex - 1]))
            startIndex--;

        var endIndex = index;
        while (char.IsNumber(text[endIndex]))
            endIndex++;

        result = int.Parse(text[startIndex..endIndex]);
        index = endIndex - 1;
        return true;
    }
}