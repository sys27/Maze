using System;

namespace Maze
{
    public readonly struct Position : IEquatable<Position>
    {
        public Position(int x, int y)
            => (X, Y) = (x, y);

        public bool Equals(Position other)
            => X == other.X && Y == other.Y;

        public override bool Equals(object obj)
            => obj is Position other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(X, Y);

        public static bool operator ==(Position left, Position right)
            => left.Equals(right);

        public static bool operator !=(Position left, Position right)
            => !left.Equals(right);

        public override string ToString()
            => $"{X}, {Y}";

        public Position Left()
            => new Position(X - 1, Y);

        public Position Right()
            => new Position(X + 1, Y);

        public Position Top()
            => new Position(X, Y - 1);

        public Position Down()
            => new Position(X, Y + 1);

        public double Distance(Position other)
            => Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));

        public int X { get; }
        public int Y { get; }
    }
}