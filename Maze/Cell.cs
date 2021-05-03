using System;

namespace Maze
{
    public class Cell : IEquatable<Cell>
    {
        public Cell(CellKind type, int x, int y)
        {
            Type = type;
            Position = new Position(x, y);
        }

        public bool Equals(Cell other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return Type == other.Type && Position == other.Position;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;

            return Equals((Cell)obj);
        }

        public static bool operator ==(Cell left, Cell right)
            => Equals(left, right);

        public static bool operator !=(Cell left, Cell right)
            => !Equals(left, right);

        public override int GetHashCode()
            => HashCode.Combine(Type, Position);

        public override string ToString()
            => $"{Type}: {Position}";

        public bool IsWall()
            => Type == CellKind.Wall;

        public bool IsPass()
            => Type == CellKind.Pass;

        public CellKind Type { get; }
        public Position Position { get; }
    }
}