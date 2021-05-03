using System.Collections.Generic;

namespace Maze
{
    public class PathNode
    {
        public PathNode(Cell cell) : this(cell, null)
        {
        }

        private PathNode(Cell cell, PathNode parent)
        {
            Cell = cell;
            Parent = parent;
        }

        public PathNode Append(Cell cell)
            => new PathNode(cell, this);

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
    }
}