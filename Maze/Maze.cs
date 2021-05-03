using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze
{
    public class Maze
    {
        private readonly Cell[][] cells;

        public Maze(Cell[][] cells) => this.cells = cells;

        public static Maze FromImage(Image<Rgba32> image)
        {
            var width = image.Width;
            var height = image.Height;
            var pixels = new Cell[height][];
            for (var y = 0; y < height; y++)
                pixels[y] = new Cell[width];

            var white = Rgba32.ParseHex("FFFFFF");

            for (var y = 0; y < image.Height; y++)
            for (var x = 0; x < image.Width; x++)
                pixels[y][x] = new Cell(image[x, y] == white ? CellKind.Pass : CellKind.Wall, x, y);

            return new Maze(pixels);
        }

        public IEnumerable<Cell> GetNeighbors(Position position)
        {
            if (position.X < 0 || position.X >= Width)
                throw new ArgumentOutOfRangeException(nameof(position.X));
            if (position.Y < 0 || position.Y >= Height)
                throw new ArgumentOutOfRangeException(nameof(position.Y));

            var neighbors = new List<Cell>(4);

            // TODO:
            var leftPosition = position.Left();
            var left = GetNeighbor(leftPosition);
            if (left != null)
                neighbors.Add(left);

            var rightPosition = position.Right();
            var right = GetNeighbor(rightPosition);
            if (right != null)
                neighbors.Add(right);

            var topPosition = position.Top();
            var top = GetNeighbor(topPosition);
            if (top != null)
                neighbors.Add(top);

            var downPosition = position.Down();
            var down = GetNeighbor(downPosition);
            if (down != null)
                neighbors.Add(down);

            return neighbors;
        }

        private Cell GetNeighbor(Position position)
        {
            if (IsPositionValid(position))
            {
                var cell = this[position];
                if (cell.IsPass())
                    return cell;
            }

            return null;
        }

        private bool IsPositionValid(Position position)
            => position.X >= 0 &&
               position.X < Width &&
               position.Y >= 0 &&
               position.Y < Height;

        public Cell this[Position position] => cells[position.Y][position.X];

        public int Width => cells.Length > 0 ? cells[0].Length : 0;
        public int Height => cells.Length;

        // TODO:
        public Position StartPosition => new Position(0, 1);
        public Position EndPosition => new Position(Width - 1, Height - 2);

        public Cell Start => this[StartPosition];
        public Cell End => this[EndPosition];
    }
}