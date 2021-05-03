using System;
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
                throw new Exception();

            using var image = await Image.LoadAsync<Rgba32>(args[0]);
            var maze = Maze.FromImage(image);
            var bfs = new Bfs();
            var solution = bfs.Solve(maze);

            using var solved = image.Clone();
            var red = Rgba32.ParseHex("FF0000");
            foreach (var cell in solution)
            {
                var position = cell.Position;

                solved[position.X, position.Y] = red;
            }

            await solved.SaveAsync("solved.png");
        }
    }
}