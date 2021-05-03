using System;
using System.Collections.Generic;

namespace Maze
{
    public class Bfs
    {
        public IEnumerable<Cell> Solve(Maze maze)
        {
            var queue = new Queue<PathNode>();
            var visited = new HashSet<Cell>();

            var startCell = maze.Start;
            var endCell = maze.End;

            queue.Enqueue(new PathNode(startCell));

            while (queue.Count > 0)
            {
                var pathNode = queue.Dequeue();
                var cell = pathNode.Cell;

                if (cell.Equals(endCell))
                    return pathNode.GetPath();

                if (!visited.Contains(cell))
                {
                    var neighbors = maze.GetNeighbors(cell.Position);
                    foreach (var neighbor in neighbors)
                    {
                        var newPath = pathNode.Append(neighbor);
                        queue.Enqueue(newPath);
                    }
                }

                visited.Add(cell);
            }

            throw new Exception("No solution.");
        }
    }
}