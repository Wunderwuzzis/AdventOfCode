namespace AoC.Utils;

public struct Vector2Int : IEquatable<Vector2Int>
{
    public int X = 0;
    public int Y = 0;

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        => new(a.X + b.X, a.Y + b.Y);

    public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        => new(a.X - b.X, a.Y - b.Y);

    public static Vector2Int operator *(Vector2Int a, int scalar)
        => new(a.X * scalar, a.Y * scalar);

    public bool Equals(Vector2Int other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector2Int other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Vector2Int left, Vector2Int right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Vector2Int left, Vector2Int right)
    {
        return !(left == right);
    }
}