using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze
{
    public class Dijkstra
    {
        public IEnumerable<Cell> Solve(Maze maze)
        {
            var startCell = maze.Start;
            var endCell = maze.End;

            var openSet = new Dictionary<Cell, int>
            {
                [startCell] = 0
            };

            var cameFrom = new Dictionary<Cell, Cell>();

            var gScore = new Dictionary<Cell, int>
            {
                [startCell] = 0
            };

            while (openSet.Count > 0)
            {
                var node = openSet.OrderBy(x => x.Value).First();
                var current = node.Key;
                if (current.Equals(endCell))
                    return ReconstructPath(cameFrom, current);

                openSet.Remove(current);

                var neighbors = maze.GetNeighbors(current.Position);
                foreach (var neighbor in neighbors)
                {
                    if (!gScore.TryGetValue(neighbor, out var neighborGScore))
                        neighborGScore = int.MaxValue;

                    var tentativeScore = neighborGScore + current.Weight;
                    if (tentativeScore < neighborGScore)
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeScore;
                        openSet[neighbor] = tentativeScore;
                    }
                }
            }

            throw new Exception("No solution.");
        }

        private IEnumerable<Cell> ReconstructPath(Dictionary<Cell, Cell> cameFrom, Cell current)
        {
            var totalPath = new LinkedList<Cell>();
            totalPath.AddLast(current);

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.AddFirst(current);
            }

            return totalPath;
        }
    }
}