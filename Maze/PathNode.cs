using System.Collections.Generic;

namespace Maze
{
    public class PathNode
    {
        public PathNode(Cell cell) : this(cell, null, 0)
        {
        }

        private PathNode(Cell cell, PathNode parent, int weight)
        {
            Cell = cell;
            Parent = parent;
            Weight = weight;
        }

        public PathNode Append(Cell cell)
            => new PathNode(cell, this, Weight + cell.Weight);

        public IEnumerable<Cell> GetPath()
        {
            var path = new LinkedList<Cell>();

            var current = this;
            do
            {
                path.AddFirst(current.Cell);

                current = current.Parent;
            } while (current != null);

            return path;
        }

        public Cell Cell { get; }
        public PathNode Parent { get; }
        public int Weight { get; }
    }
}