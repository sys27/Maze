using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0)
                args = new[] { "200x200.png" };

            using var image = await Image.LoadAsync<Rgba32>(args[0]);
            var maze = Maze.FromImage(image);

            var sw = Stopwatch.StartNew();

            var bfs = new Bfs();
            var solution = bfs.Solve(maze);

            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms.");

            sw = Stopwatch.StartNew();

            var dijkstra = new Dijkstra();
            solution = dijkstra.Solve(maze);

            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms.");

            // using var solved = image.Clone();
            // var red = Rgba32.ParseHex("FF0000");
            // foreach (var cell in solution)
            // {
            //     var position = cell.Position;
            //
            //     solved[position.X, position.Y] = red;
            // }
            //
            // await solved.SaveAsync("solved.png");
        }
    }
}