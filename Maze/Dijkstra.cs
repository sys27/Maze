using System;
using System.Collections.Generic;

namespace Maze
{
    public class Dijkstra
    {
        public IEnumerable<Cell> Solve(Maze maze)
        {
            var queue = new Dictionary<Cell, PathNode>();
            var visited = new HashSet<Cell>(maze.Height * maze.Width);

            var startCell = maze.Start;
            var endCell = maze.End;

            queue[startCell] = new PathNode(startCell);

            while (queue.Count > 0)
            {
                var pathNode = queue.Values.MinBy(x => x.Weight);
                queue.Remove(pathNode.Cell);

                var cell = pathNode.Cell;

                if (cell.Equals(endCell))
                    return pathNode.GetPath();

                if (!visited.Contains(cell))
                {
                    var neighbors = maze.GetNeighbors(cell.Position);
                    foreach (var neighbor in neighbors)
                    {
                        var newPath = pathNode.Append(neighbor);
                        if (queue.TryGetValue(neighbor, out var existingPath))
                        {
                            if (newPath.Weight < existingPath.Weight)
                                queue[neighbor] = newPath;
                        }
                        else
                        {
                            queue[neighbor] = newPath;
                        }
                    }
                }

                visited.Add(cell);
            }

            throw new Exception("No solution.");
        }
    }
}